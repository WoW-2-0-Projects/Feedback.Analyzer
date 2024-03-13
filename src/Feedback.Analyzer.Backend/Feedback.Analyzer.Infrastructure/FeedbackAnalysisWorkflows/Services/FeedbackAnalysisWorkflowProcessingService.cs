using AutoMapper;
using Feedback.Analyzer.Application.Common.AnalysisWorkflows.Services;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Services;

/// <summary>
/// Service responsible for processing feedback analysis workflows.
/// </summary>
public class FeedbackAnalysisWorkflowProcessingService(
    IMapper mapper,
    IFeedbackAnalysisWorkflowService feedbackAnalysisWorkflowService,
    IAnalysisWorkflowService analysisWorkflowService
)
{
    public async ValueTask<(FeedbackAnalysisWorkflow FeedbackAnalysisWorkflow, AnalysisWorkflow AnalysisWorkflow)> CreateAsync(
            FeedbackAnalysisWorkflow feedbackAnalysisWorkflow,
            AnalysisWorkflow analysisWorkflow,
            CancellationToken cancellationToken = default)
    {
        var createdAnalysisWorkflow =
            await analysisWorkflowService.CreateAsync(analysisWorkflow, cancellationToken: cancellationToken);

        feedbackAnalysisWorkflow.Id = createdAnalysisWorkflow.Id;

        return (await feedbackAnalysisWorkflowService.CreateAsync(feedbackAnalysisWorkflow,
            cancellationToken: cancellationToken), analysisWorkflow);
    }

    public async ValueTask<(FeedbackAnalysisWorkflow FeedbackAnalysisWorkflow, AnalysisWorkflow AnalysisWorkflow)> UpdateAsync(
            FeedbackAnalysisWorkflow feedbackAnalysisWorkflow,
            AnalysisWorkflow analysisWorkflow,
            CancellationToken cancellationToken = default)
    {
        var updatedAnalysisWorkflow = await analysisWorkflowService
            .UpdateAsync(analysisWorkflow, cancellationToken: cancellationToken);

        return (feedbackAnalysisWorkflow, updatedAnalysisWorkflow);
    }
}