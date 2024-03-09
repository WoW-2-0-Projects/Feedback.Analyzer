using Feedback.Analyzer.Application.Common.Prompts.Events;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Infrastructure.Common.Prompts.EventHandlers;

// public class ExecutePromptEventHandler(IAnalysisWorkflowOrchestrationService executionOrchestrationService)
//     : IEventHandler<ExecutePromptEvent>
// {
//     public async Task Handle(ExecutePromptEvent notification, CancellationToken cancellationToken)
//     {
//         await executionOrchestrationService.ExecuteAsync(notification.AnalysisWorkflowContext, cancellationToken);
//     }
// }