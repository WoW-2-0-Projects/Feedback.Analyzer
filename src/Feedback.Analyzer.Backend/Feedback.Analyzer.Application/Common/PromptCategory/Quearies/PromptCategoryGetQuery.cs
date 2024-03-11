using Feedback.Analyzer.Application.Common.PromptCategory.Models;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Common.PromptCategory.Quearies;

/// <summary>
///  Represents a query to retrieve a collection of organizations.
/// </summary>
public record PromptCategoryGetQuery : IQuery<ICollection<AnalysisPromptCategoryDto>>
{
    /// <summary>
    ///  Gets or sets the filter to apply when retrieving organizations.
    /// </summary>
    public PromptCategoryFilter Filter { get; set; } = new();
}