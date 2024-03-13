using AutoMapper;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Commands;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.CommandHandlers;

/// <summary>
/// Represents a command handler for deleting a feedback analysis workflow by its ID.
/// </summary>
public class FeedbackAnalysisWorkflowDeleteByIdCommandHandler(IMapper mapper, IFeedbackAnalysisWorkflowService feedbackAnalysisWorkflowService)
    : ICommandHandler<FeedbackAnalysisWorkflowDeleteByIdCommand, bool>
{
    public async Task<bool> Handle(FeedbackAnalysisWorkflowDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        await feedbackAnalysisWorkflowService.DeleteByIdAsync(request.Id, cancellationToken: cancellationToken);
        return true;
    }
}