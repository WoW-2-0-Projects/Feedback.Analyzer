using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Common.Prompts.Queries;

/// <summary>
/// Represents a query to retrieve a specific organization by its ID.
/// </summary>
public record PromptGetByIdQuery : IQuery<AnalysisPromptDto?>
{
    /// <summary>
    /// The unique identifier of the organization to retrieve.
    /// </summary>
    public Guid PromptId { get; set; }
}