using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Common.PromptResults.Queries;

/// <summary>
/// Represents a query to retrieve prompt results by prompt ID.
/// </summary>
public class PromptResultGetByPromptIdQuery : IQuery<PromptResultDto?>
{
    /// <summary>
    /// Gets or sets prompt Id to retrieve results for
    /// </summary>
    public Guid PromptId { get; set; }
}