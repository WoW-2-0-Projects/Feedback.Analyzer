using Feedback.Analyzer.Application.FeedbackWorkflows.Commands;
using Feedback.Analyzer.Application.FeedbackWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Infrastructure.FeedbackWorkflows.CommandHandlers;

/// <summary>
/// Represents a command handler responsible for deleting a feedback workflow by its ID.
/// </summary>
public class FeedbackWorkflowDeleteByIdCommandHandler(IFeedbackWorkflowService feedbackWorkflowService)
: ICommandHandler<FeedbackWorkflowDeleteByIdCommand, bool>
{
    public async Task<bool> Handle(FeedbackWorkflowDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        await feedbackWorkflowService.DeleteByIdAsync(request.FeedbackWorkflowId, cancellationToken: cancellationToken);
        return true;
    }
}