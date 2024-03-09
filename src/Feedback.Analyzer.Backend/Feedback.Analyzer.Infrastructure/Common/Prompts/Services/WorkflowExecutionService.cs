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
        WorkflowExecutionOption headPromptOption,
        PromptExecutionContext promptExecutionContext,
        // CustomerFeedback feedback,
        CancellationToken cancellationToken = default
    )
    {
        throw new NotImplementedException();
        // await ExecutePrompt(executionContext, headPromptOption);
    }

    private async ValueTask ExecutePrompt(PromptExecutionContext executionContext, WorkflowExecutionOption executionOption)
    {
        if (executionContext.Arguments is null)
            return;

        await appDbContext.Entry(executionOption).Reference(options => options.AnalysisPromptCategory).LoadAsync();

        var history = promptExecutionProcessingService.ExecuteAsync(
                (Guid)executionOption.AnalysisPromptCategory.SelectedPromptId!,
                executionContext.Arguments
            )
            .Result[0];

        executionContext.ExecutionHistories.Add(executionOption.Id, history);

        if (executionOption.AnalysisPromptCategory.Category == FeedbackAnalysisPromptCategory.RelevanceAnalysis)
        {
            var test = JsonConvert.DeserializeObject<FeedbackRelevance>(history.Result!.ToLower());
            if (!test?.IsRelevant ?? false)
                executionContext.Arguments = null!;
        }

        else if (executionOption.AnalysisPromptCategory.Category == FeedbackAnalysisPromptCategory.RelevantContentExtraction)
        {
            var test = JsonConvert.DeserializeObject<FeedbackRelevance>(history.Result!);
            executionContext.Arguments[PromptConstants.CustomerFeedback] = test!.ExtractedRelevantContent;
        }

        else if (executionOption.AnalysisPromptCategory.Category == FeedbackAnalysisPromptCategory.PersonalInformationRedaction)
        {
            var test = JsonConvert.DeserializeObject<FeedbackRelevance>(history.Result!);
            executionContext.Arguments[PromptConstants.CustomerFeedback] = test!.PiiRedactedContent;
        }

        else if (executionOption.AnalysisPromptCategory.Category is FeedbackAnalysisPromptCategory.QuestionPointsExtraction
                 or FeedbackAnalysisPromptCategory.OpinionPointsExtraction)
        {
            var test = JsonConvert.DeserializeObject<string[]>(history.Result!);
            executionContext.Arguments[PromptConstants.CustomerFeedback] = string.Join(", ", test!);
        }

        await appDbContext.Entry(executionOption).Collection(option => option.ChildExecutionOptions).LoadAsync();

        if (executionOption.ChildExecutionOptions.Any())
            await ExecuteChildrenPrompts(executionContext, executionOption.ChildExecutionOptions.ToImmutableList());
    }

    private async ValueTask ExecuteChildrenPrompts(PromptExecutionContext executionContext, IImmutableList<WorkflowExecutionOption> childrenOptions)
    {
        foreach (var prompt in childrenOptions.Where(prompt => !prompt.IsDisabled))
        {
            await ExecutePrompt(executionContext, prompt);
        }
    }

    public ValueTask ExecuteAsync(WorkflowExecutionOption headPromptOption, CustomerFeedback feedback, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

public class PromptExecutionContext
{
    public Dictionary<string, string> Arguments { get; set; }

    public Dictionary<Guid, PromptExecutionHistory> ExecutionHistories { get; set; }
}