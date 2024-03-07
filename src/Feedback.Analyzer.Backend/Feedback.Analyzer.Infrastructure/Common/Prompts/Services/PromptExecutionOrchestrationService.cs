using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Newtonsoft.Json;

namespace Feedback.Analyzer.Infrastructure.Common.Prompts.Services;

public class PromptExecutionOrchestrationService(IPromptExecutionProcessingService promptExecutionProcessingService) : IPromptExecutionOrchestrationService
{
    public async ValueTask ExecuteAsync(WorkflowPromptCategoryExecutionOptions headPromptOption, CustomerFeedback feedback,
        CancellationToken cancellationToken = default)
    {
        
        var executionContext = new PromptExecutionContext()
        {
            Arguments = new Dictionary<string, string>
            {
                { "productDescription", feedback.Product.Description },
                { "customerFeedback", feedback.Comment }
            },
            ExecutionHistories = new Dictionary<Guid, PromptExecutionHistory>(),
        };

        await ExecuteWorkflowPrompts(executionContext, headPromptOption);
        
        throw new NotImplementedException();
    }

    private async ValueTask ExecuteWorkflowPrompts(PromptExecutionContext executionContext, WorkflowPromptCategoryExecutionOptions executionOptions)
    {
        await ExecutePrompt(executionContext, executionOptions);

        foreach (var prompt in executionOptions.ChildExecutionOptions.Where(prompt => prompt.IsActive))
        {
            await ExecutePrompt(executionContext, prompt);
            await ExecuteWorkflowPrompts(executionContext, prompt);
        }
    }

    private ValueTask ExecutePrompt(PromptExecutionContext executionContext, WorkflowPromptCategoryExecutionOptions executionOptions)
    {
        if (executionContext.Arguments is null)
            return ValueTask.CompletedTask;

        var history = promptExecutionProcessingService.ExecuteAsync((Guid)executionOptions.AnalysisPromptCategory.SelectedPromptId!,
                executionContext.Arguments).Result[0];
        
        executionContext.ExecutionHistories.Add(executionOptions.Id, history);

        if (executionOptions.AnalysisPromptCategory.Category  == FeedbackAnalysisPromptCategory.RelevanceAnalysis)
        {
            var test = JsonConvert.DeserializeObject<bool>(history.Result!);
            if (!test)
                executionContext.Arguments = null!;
        }

        else if (executionOptions.AnalysisPromptCategory.Category  == FeedbackAnalysisPromptCategory.RelevantContentExtraction)
        {
            var test = JsonConvert.DeserializeObject<string>(history.Result!);
            executionContext.Arguments["customerFeedback"] = test;
        }

        else if (executionOptions.AnalysisPromptCategory.Category is FeedbackAnalysisPromptCategory.QuestionPointsExtraction or FeedbackAnalysisPromptCategory.OpinionPointsExtraction)
        {
            var test = JsonConvert.DeserializeObject<string[]>(history.Result!);
            executionContext.Arguments["customerFeedback"] = string.Join(", ", test!);
        }

        return ValueTask.CompletedTask;
    }
}

public class PromptExecutionContext
{
    public Dictionary<string, string> Arguments { get; set; }

    public Dictionary<Guid, PromptExecutionHistory> ExecutionHistories { get; set; }
}