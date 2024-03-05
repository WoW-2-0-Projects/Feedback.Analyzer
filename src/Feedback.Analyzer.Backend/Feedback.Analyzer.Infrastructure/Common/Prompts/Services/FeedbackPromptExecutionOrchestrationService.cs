using System.Collections.Immutable;
using System.Diagnostics;
using Feedback.Analyzer.Application.Common.Prompts.Brokers;
using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Application.Common.PromptsHistory.Services;
using Feedback.Analyzer.Application.CustomerFeedbacks.Services;
using Feedback.Analyzer.Application.Products.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Exceptions;
using Feedback.Analyzer.Domain.Entities;
using Microsoft.SemanticKernel;
using Newtonsoft.Json;

namespace Feedback.Analyzer.Infrastructure.Common.Prompts.Services;

public class FeedbackPromptExecutionOrchestrationService(
    IPromptService promptService,
    ICustomerFeedbackService customerFeedbackService,
    IProductService productService,
    IPromptExecutionBroker promptExecutionBroker,
    IPromptsExecutionHistoryService promptsExecutionHistoryService
) : IFeedbackPromptExecutionOrchestrationService
{
    public async ValueTask ExecuteAsync(FeedbackExecutionContext feedbackExecutionContext, CancellationToken cancellationToken = default)
    {
        var feedback = await customerFeedbackService.GetByIdAsync(feedbackExecutionContext.FeedbackId, cancellationToken: cancellationToken) ??
                       throw new InvalidOperationException(
                           $"Could not execute prompt, feedback with id {feedbackExecutionContext.FeedbackId} not found."
                       );

        var product = await productService.GetByIdAsync(feedbackExecutionContext.ProductId, cancellationToken: cancellationToken) ??
                      throw new InvalidOperationException(
                          $"Could not execute prompt, product with id {feedbackExecutionContext.ProductId} not found."
                      );

        var kernelArguments = new KernelArguments
        {
            { "productDescription", product.Description },
            { "customerFeedback", feedback.Comment }
        };

        var task = feedbackExecutionContext switch
        {
            FeedbackSingleExecutionContext executionContext => ExecuteAsync(executionContext, kernelArguments, cancellationToken),
            FeedbackWorkflowExecutionContext executionContext => ExecuteAsync(executionContext, kernelArguments, cancellationToken),
            _ => throw new InvalidOperationException("Invalid feedback execution context.")
        };


        await task;
    }

    private async ValueTask ExecuteAsync(
        FeedbackSingleExecutionContext executionContext,
        KernelArguments kernelArguments,
        CancellationToken cancellationToken = default
    )
    {
        // Query prompt and feedback
        var prompt = await promptService.GetByIdAsync(executionContext.PromptId, cancellationToken: cancellationToken) ??
                     throw new InvalidOperationException($"Could not execute prompt, prompt with id {executionContext.PromptId} not found.");


        // Execute prompt
        var executionResults = new List<(FuncResult<string?> PromptResult, double Elapsed)>();

        await Parallel.ForEachAsync(
            Enumerable.Range(0, (int)executionContext.ExecutionCount),
            cancellationToken,
            async (_, _) =>
            {
                var stopWatch = new Stopwatch();
                var promptResult = await promptExecutionBroker.ExecutePromptAsync(prompt.Prompt, kernelArguments, cancellationToken);
                stopWatch.Stop();
                var elapsedSeconds = stopWatch.Elapsed.TotalSeconds;

                // Add result to list
                executionResults.Add((promptResult, elapsedSeconds));
            }
        );

        // Map to history
        var histories = executionResults.Select(result => MapToHistory(executionContext.PromptId, result.PromptResult, result.Elapsed))
            .ToImmutableList();

        // Save prompt execution history
        var createHistoryCommandOptions = new CommandOptions
        {
            SkipSaveChanges = true
        };

        foreach (var history in histories.SkipLast(1))
            await promptsExecutionHistoryService.CreateAsync(history, createHistoryCommandOptions, cancellationToken);

        await promptsExecutionHistoryService.CreateAsync(histories.Last(), default, cancellationToken: cancellationToken);
    }

    private ValueTask ExecuteAsync(
        FeedbackWorkflowExecutionContext executionContext,
        KernelArguments kernelArguments,
        CancellationToken cancellationToken = default
    )
    {
        throw new NotImplementedException();
    }
    
    private PromptExecutionHistory MapToHistory(Guid promptId, FuncResult<string?> result, double elapsed)
    {
        return new PromptExecutionHistory
        {
            PromptId = promptId,
            Result = result.Data,
            Exception = result.Exception is not null ? JsonConvert.SerializeObject(result.Exception) : null,
            ExecutionTime = DateTime.UtcNow,
            ExecutionDuration = TimeSpan.FromSeconds(elapsed)
        };
    }
}