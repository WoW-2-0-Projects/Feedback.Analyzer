using System.Collections.Immutable;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Domain.Constants;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Persistence.DataContexts;
using Newtonsoft.Json;

namespace Feedback.Analyzer.Infrastructure.Common.Prompts.Services;

public class WorkflowExecutionService(
    IPromptExecutionProcessingService promptExecutionProcessingService, 
    AppDbContext appDbContext)
    : IWorkflowExecutionService
{
    public async ValueTask ExecuteAsync(
        WorkflowExecutionOptions headPromptOption,
        PromptExecutionContext promptExecutionContext,
        // CustomerFeedback feedback,
        CancellationToken cancellationToken = default
    )
    {
        throw new NotImplementedException();
        // await ExecutePrompt(executionContext, headPromptOption);
    }

    private async ValueTask ExecutePrompt(PromptExecutionContext executionContext, WorkflowExecutionOptions executionOptions)
    {
        if (executionContext.Arguments is null)
            return;

        await appDbContext.Entry(executionOptions).Reference(options => options.AnalysisPromptCategory).LoadAsync();

        var history = promptExecutionProcessingService.ExecuteAsync(
                (Guid)executionOptions.AnalysisPromptCategory.SelectedPromptId!,
                executionContext.Arguments
            )
            .Result[0];

        executionContext.ExecutionHistories.Add(executionOptions.Id, history);

        if (executionOptions.AnalysisPromptCategory.Category == FeedbackAnalysisPromptCategory.RelevanceAnalysis)
        {
            var test = JsonConvert.DeserializeObject<FeedbackRelevance>(history.Result!.ToLower());
            if (!test?.IsRelevant ?? false)
                executionContext.Arguments = null!;
        }

        else if (executionOptions.AnalysisPromptCategory.Category == FeedbackAnalysisPromptCategory.RelevantContentExtraction)
        {
            var test = JsonConvert.DeserializeObject<FeedbackRelevance>(history.Result!);
            executionContext.Arguments[PromptConstants.CustomerFeedback] = test!.ExtractedRelevantContent;
        }

        else if (executionOptions.AnalysisPromptCategory.Category == FeedbackAnalysisPromptCategory.PersonalInformationRedaction)
        {
            var test = JsonConvert.DeserializeObject<FeedbackRelevance>(history.Result!);
            executionContext.Arguments[PromptConstants.CustomerFeedback] = test!.PiiRedactedContent;
        }

        else if (executionOptions.AnalysisPromptCategory.Category is FeedbackAnalysisPromptCategory.QuestionPointsExtraction
                 or FeedbackAnalysisPromptCategory.OpinionPointsExtraction)
        {
            var test = JsonConvert.DeserializeObject<string[]>(history.Result!);
            executionContext.Arguments[PromptConstants.CustomerFeedback] = string.Join(", ", test!);
        }

        await appDbContext.Entry(executionOptions).Collection(option => option.ChildExecutionOptions).LoadAsync();

        if (executionOptions.ChildExecutionOptions.Any())
            await ExecuteChildrenPrompts(executionContext, executionOptions.ChildExecutionOptions.ToImmutableList());
    }

    private async ValueTask ExecuteChildrenPrompts(PromptExecutionContext executionContext, IImmutableList<WorkflowExecutionOptions> childrenOptions)
    {
        foreach (var prompt in childrenOptions.Where(prompt => !prompt.IsDisabled))
        {
            await ExecutePrompt(executionContext, prompt);
        }
    }

    public ValueTask ExecuteAsync(WorkflowExecutionOptions headPromptOption, CustomerFeedback feedback, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

public class PromptExecutionContext
{
    public Dictionary<string, string> Arguments { get; set; }

    public Dictionary<Guid, PromptExecutionHistory> ExecutionHistories { get; set; }
}