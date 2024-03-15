// using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Events;
// using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
// using Feedback.Analyzer.Domain.Common.Events;
// using MassTransit;
// using Microsoft.Extensions.DependencyInjection;
//
// namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.EventHandlers;
//
// public class AnalyzeFeedbackEventHandler(IServiceScopeFactory serviceScopeFactory) : IEventHandler<AnalyzeFeedbackEvent>
// {
//     public async Task Handle(AnalyzeFeedbackEvent notification, CancellationToken cancellationToken)
//     {
//         var scopedServiceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
//         var feedbackAnalysisOrchestrationService = scopedServiceProvider.GetRequiredService<IFeedbackAnalysisOrchestrationService>();
//
//         await feedbackAnalysisOrchestrationService.ExecuteWorkflowAsync(notification.Context, cancellationToken);
//     }
//
//     public Task Consume(ConsumeContext<AnalyzeFeedbackEvent> context)
//     {
//         throw new NotImplementedException();
//     }
// }