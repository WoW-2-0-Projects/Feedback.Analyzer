using Feedback.Analyzer.Domain.Common.Charts;
using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents feedback analysis workflow result report
/// </summary>
public sealed class FeedbackAnalysisWorkflowResultStats : ChartDiscreteData, IEntity
{
    public Guid Id { get; set; }

    /// <summary>
    /// Gets type of the statistics
    /// </summary>
    public ChartDataTypeColor Type { get; init; }

    /// <summary>
    /// Gets or sets related workflow result id
    /// </summary>
    public Guid WorkflowResultId { get; set; }
}