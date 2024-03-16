﻿using Feedback.Analyzer.Application.Common.AnalysisWorkflows.Services;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Services;

/// <summary>
/// Provides feedback analysis processing service functionality
/// </summary>
public class FeedbackAnalysisWorkflowProcessingService(
    IFeedbackAnalysisWorkflowService feedbackAnalysisWorkflowService,
    IAnalysisWorkflowService analysisWorkflowService
) : IFeedbackAnalysisWorkflowProcessingService
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