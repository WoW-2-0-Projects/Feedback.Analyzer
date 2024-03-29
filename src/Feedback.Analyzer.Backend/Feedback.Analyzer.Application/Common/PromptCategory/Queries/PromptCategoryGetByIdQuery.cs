using Feedback.Analyzer.Application.Common.PromptCategory.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Common.PromptCategory.Queries;

/// <summary>
/// Represents a query to retrieve an analysis prompt category by its unique identifier.
/// </summary>
public class PromptCategoryGetByIdQuery : IQuery<AnalysisPromptCategoryDto?>
{
    /// <summary>
    /// Gets or sets the unique identifier of the category to retrieve.
    /// </summary>
    public Guid CategoryId { get; set; }
}