using Feedback.Analyzer.Application.FeedbackAnalysisResults.Models;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.FeedbackAnalysisResults.Commands;

/// <summary>
/// Represents a command for creating a feedback analysis result.
/// </summary>
public record FeedbackAnalysisResultCreateCommand : ICommand<FeedbackAnalysisResultDto>
{
    /// <summary>
    /// Gets or sets the feedback analysis result DTO.
    /// </summary>
    public FeedbackAnalysisResultDto FeedbackAnalysisResult { get; set; } = default!;
}