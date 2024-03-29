using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.Common.Prompts.Commands;

/// <summary>
/// Represents a command to delete an existing organization.
/// </summary>
public record PromptDeleteByIdCommand : ICommand<bool>
{
    /// <summary>
    /// The unique identifier of the organization to be deleted.
    /// </summary>
    public Guid PromptId { get; set; }
}