using System.Collections.Immutable;
using System.Diagnostics;
using Feedback.Analyzer.Application.Common.Prompts.Brokers;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Application.PromptsHistory.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Exceptions;
using Feedback.Analyzer.Domain.Entities;
using Microsoft.SemanticKernel;
using Newtonsoft.Json;

namespace Feedback.Analyzer.Infrastructure.Common.Prompts.Services;

public class PromptExecutionProcessingService(
    IPromptService promptService,
    IPromptExecutionBroker promptExecutionBroker,
    IPromptExecutionHistoryService promptsExecutionHistoryService
) : IPromptExecutionProcessingService
{
    public async ValueTask<IImmutableList<PromptExecutionHistory>> ExecuteAsync(
        Guid promptId,
        Dictionary<string, string> arguments,
        uint executionCount = 1,
        CancellationToken cancellationToken = default
    )
    {
        // Query prompt and feedback
        var prompt = await promptService.GetByIdAsync(promptId, cancellationToken: cancellationToken) ??
                     throw new InvalidOperationException($"Could not execute prompt, prompt with id {promptId} not found.");

        // Map arguments to kernel arguments
        var kernelArguments = new KernelArguments();
        foreach (var keyValuePair in arguments)
            kernelArguments.Add(keyValuePair.Key, keyValuePair.Value);

        // Execute prompt
        var executionResults = new List<(FuncResult<string?> PromptResult, double ElapsedMilliseconds)>();

        await Parallel.ForEachAsync(
            Enumerable.Range(0, (int)executionCount),
            cancellationToken,
            async (_, _) =>
            {
                var stopWatch = new Stopwatch();
                var promptResult = await promptExecutionBroker.ExecutePromptAsync(prompt.Prompt, kernelArguments, cancellationToken);
                stopWatch.Stop();
                var elapsedMilliseconds = stopWatch.Elapsed.TotalMilliseconds;

                // Add result to list
                executionResults.Add((promptResult, elapsedMilliseconds));
            }
        );

        // Map to history
        var histories = executionResults.Select(result => MapToHistory(promptId, result.PromptResult, result.ElapsedMilliseconds)).ToImmutableList();

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

    private PromptExecutionHistory MapToHistory(Guid promptId, FuncResult<string?> result, double elapsedMilliseconds)
    {
        return new PromptExecutionHistory
        {
            PromptId = promptId,
            Result = result.Data,
            Exception = result.Exception is not null ? JsonConvert.SerializeObject(result.Exception) : null,
            ExecutionTime = DateTime.UtcNow,
            ExecutionDuration = TimeSpan.FromMilliseconds(elapsedMilliseconds)
        };
    }
}