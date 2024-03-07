using Feedback.Analyzer.Application.AnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.AnalysisWorkflows.Queries;

/// <summary>
/// Represents a query for retrieving an analysis workflow by its ID.
/// </summary>
public class AnalysisWorkflowGetByIdQuery : IQuery<AnalysisWorkflowDto>
{
    /// <summary>
    /// Gets or sets the ID of the analysis workflow to retrieve.
    /// </summary>
    public Guid AnalysisWorkflowId { get; set; } = Guid.Empty;
}
