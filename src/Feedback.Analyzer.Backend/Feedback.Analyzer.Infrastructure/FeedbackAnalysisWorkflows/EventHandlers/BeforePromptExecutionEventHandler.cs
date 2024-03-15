// using Feedback.Analyzer.Application.Common.Prompts.Events;
// using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
// using Feedback.Analyzer.Domain.Common.Events;
// using Feedback.Analyzer.Domain.Constants;
// using Feedback.Analyzer.Domain.Enums;
// using MassTransit;
//
// namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.EventHandlers;
//
// /// <summary>
// ///  Represents event handler for before prompt execution hook event
// /// </summary>
// public class BeforePromptExecutionEventHandler : IEventHandler<BeforePromptExecutionEvent<SingleFeedbackAnalysisWorkflowContext>>
// {
//     public Task Handle(BeforePromptExecutionEvent<SingleFeedbackAnalysisWorkflowContext> notification, CancellationToken cancellationToken)
//     {
//         if (notification.Context is not SingleFeedbackAnalysisWorkflowContext context)
//             return Task.CompletedTask;
//
//         if (notification.Prompt.Category.Category == FeedbackAnalysisPromptCategory.RelevanceAnalysis)
//         {
//         }
//
//         if (notification.Prompt.Category.Category == FeedbackAnalysisPromptCategory.RelevantContentExtraction)
//         {
//         }
//
//         if (notification.Prompt.Category.Category == FeedbackAnalysisPromptCategory.PersonalInformationRedaction)
//         {
//             context.Arguments[PromptConstants.CustomerFeedback] = context.Result.FeedbackRelevance.ExtractedRelevantContent;
//         }
//
//         if (notification.Prompt.Category.Category == FeedbackAnalysisPromptCategory.OpinionMining)
//         {
//             context.Arguments[PromptConstants.CustomerFeedback] = context.Result.FeedbackRelevance.PiiRedactedContent;
//         }
//
//         return Task.CompletedTask;
//     }
//
//     public Task Consume(ConsumeContext<BeforePromptExecutionEvent<SingleFeedbackAnalysisWorkflowContext>> context)
//     {
//         throw new NotImplementedException();
//     }
// }