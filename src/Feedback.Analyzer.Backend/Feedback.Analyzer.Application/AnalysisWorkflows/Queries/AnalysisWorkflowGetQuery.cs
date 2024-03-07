using Feedback.Analyzer.Application.AnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.AnalysisWorkflows.Queries;

/// <summary>
/// Represents a query for retrieving a collection of analysis workflows based on a filter.
/// </summary>
public class AnalysisWorkflowGetQuery : IQuery<ICollection<AnalysisWorkflowDto>>
{
    /// <summary>
    /// Gets or sets the filter to apply when retrieving analysis workflows.
    /// </summary>
    public AnalysisWorkflowFilter Filter { get; set; } = default!;
}