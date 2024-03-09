using Feedback.Analyzer.Application.Common.Prompts.Events;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.EventHandlers;

/// <summary>
///  Represents event handler for before prompt execution hook event
/// </summary>
public class BeforePromptExecutionEventHandler : IEventHandler<BeforePromptExecutionEvent<SingleFeedbackAnalysisWorkflowContext>>
{
    public Task Handle(BeforePromptExecutionEvent<SingleFeedbackAnalysisWorkflowContext> notification, CancellationToken cancellationToken)
    {
        // if (executionOption.AnalysisPromptCategory.Category == FeedbackAnalysisPromptCategory.RelevanceAnalysis)
        // {
        //     var test = JsonConvert.DeserializeObject<FeedbackRelevance>(history.Result!.ToLower());
        //     if (!test?.IsRelevant ?? false)
        //         executionContext.Arguments = null!;
        // }
        //
        // else if (executionOption.AnalysisPromptCategory.Category == FeedbackAnalysisPromptCategory.RelevantContentExtraction)
        // {
        //     var test = JsonConvert.DeserializeObject<FeedbackRelevance>(history.Result!);
        //     executionContext.Arguments[PromptConstants.CustomerFeedback] = test!.ExtractedRelevantContent;
        // }
        //
        // else if (executionOption.AnalysisPromptCategory.Category == FeedbackAnalysisPromptCategory.PersonalInformationRedaction)
        // {
        //     var test = JsonConvert.DeserializeObject<FeedbackRelevance>(history.Result!);
        //     executionContext.Arguments[PromptConstants.CustomerFeedback] = test!.PiiRedactedContent;
        // }
        //
        // else if (executionOption.AnalysisPromptCategory.Category is FeedbackAnalysisPromptCategory.QuestionPointsExtraction
        //          or FeedbackAnalysisPromptCategory.OpinionPointsExtraction)
        // {
        //     var test = JsonConvert.DeserializeObject<string[]>(history.Result!);
        //     executionContext.Arguments[PromptConstants.CustomerFeedback] = string.Join(", ", test!);
        // }
        
        throw new NotImplementedException();
    }
}