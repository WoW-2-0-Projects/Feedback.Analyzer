using System.Collections.Immutable;
using System.Diagnostics;
using Feedback.Analyzer.Application.Common.Prompts.Brokers;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Application.Common.PromptsHistory.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Exceptions;
using Feedback.Analyzer.Domain.Constants;
using Feedback.Analyzer.Domain.Entities;
using Microsoft.SemanticKernel;
using Newtonsoft.Json;

namespace Feedback.Analyzer.Infrastructure.Common.Prompts.Services;

/// <summary>
/// Service for executing prompts asynchronously and handling their history.
/// </summary>
/// <param name="promptExecutionBroker">The prompt execution broker.</param>
/// <param name="promptsExecutionHistoryService">The prompt execution history service.</param>
public class PromptExecutionProcessingService(
    IPromptExecutionBroker promptExecutionBroker,
    IPromptExecutionHistoryService promptsExecutionHistoryService
) : IPromptExecutionProcessingService
{
    public async ValueTask<IImmutableList<PromptExecutionHistory>> ExecuteAsync(
        AnalysisPrompt prompt,
        Dictionary<string,string> arguments,
        uint executionCount = 1,
        CancellationToken cancellationToken = default
    )
    {
        // Execute prompt
        var executionResults = new List<(FuncResult<string?> PromptResult, double ElapsedMilliseconds)>();

        await Parallel.ForEachAsync(
            Enumerable.Range(0, (int)executionCount),
            cancellationToken,
            async (_, _) =>
            {
                var stopWatch = new Stopwatch();
                var promptResult = await promptExecutionBroker.ExecutePromptAsync(prompt.Prompt, arguments, cancellationToken);
                stopWatch.Stop();
                var elapsedMilliseconds = stopWatch.Elapsed.TotalMilliseconds;

                // Add result to list
                executionResults.Add((promptResult, elapsedMilliseconds));
            }
        );

        // Map to history
        var histories = executionResults.Select(result => MapToHistory(prompt, result.PromptResult, result.ElapsedMilliseconds)).ToImmutableList();

        var executionHistories = new List<PromptExecutionHistory>();
        
        if (histories.Count > 1)
        {
            // Save prompt execution history
            var createHistoryCommandOptions = new CommandOptions
            {
                SkipSaveChanges = true
            };

            foreach (var history in histories.SkipLast(1))
                executionHistories.Add(await promptsExecutionHistoryService.CreateAsync(history, createHistoryCommandOptions, cancellationToken));
        }

        executionHistories.Add(await promptsExecutionHistoryService.CreateAsync(histories.Last(), default, cancellationToken: cancellationToken));
      
        return executionHistories.ToImmutableList();
    }

    /// <summary>
    /// Maps prompt execution result to execution history
    /// </summary>
    /// <param name="prompt"></param>
    /// <param name="result"></param>
    /// <param name="elapsedMilliseconds"></param>
    /// <returns></returns>
    private PromptExecutionHistory MapToHistory(AnalysisPrompt prompt, FuncResult<string?> result, double elapsedMilliseconds)
    {
        return new PromptExecutionHistory
        {
            PromptId = prompt.Id,
            PromptCategoryId = prompt.CategoryId,
            Result = result.Data,
            Exception = result.Exception is not null ? JsonConvert.SerializeObject(result.Exception) : null,
            ExecutionTime = DateTime.UtcNow,
            ExecutionDuration = TimeSpan.FromMilliseconds(elapsedMilliseconds)
        };
    }
}