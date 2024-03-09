using Feedback.Analyzer.Application.Common.Workflows.Events;
using Feedback.Analyzer.Application.CustomerFeedbacks.Services;
using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Infrastructure.Common.Workflows.EventHandlers;

public class ExecuteWorkflowEventHandler(
    IIFeedbackAnalysisWorkflowOrchestrationService feedbackAnalysisWorkflowOrchestrationService,
 
    // IFeedbackAnalysisWorkflowService feedbackExecutionWorkflowService,
    // IWorkflowExecutionService workflowExecutionService
) : IEventHandler<ExecuteWorkflowEvent>
{
    public async Task Handle(ExecuteWorkflowEvent notification, CancellationToken cancellationToken)
    {
        await feedbackAnalysisWorkflowOrchestrationService.ExecuteWorkflowAsync(notification.WorkflowId, cancellationToken);

        // Query the workflow by its id, including its product and customer feedbacks
      
    }
}