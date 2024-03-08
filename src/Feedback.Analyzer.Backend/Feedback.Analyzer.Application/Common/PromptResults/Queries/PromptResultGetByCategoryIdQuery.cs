using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Common.PromptResults.Queries;

/// <summary>
/// Represents a query to retrieve prompt results by category ID.
/// </summary>
public record PromptResultGetByCategoryIdQuery : IQuery<ICollection<PromptResultDto>>
{
    public Guid CategoryId { get; set; }
}