using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.Clients.Commands;

/// <summary>
/// Represents a command to delete a client by their unique identifier.
/// </summary>
public record ClientDeleteByIdCommand : ICommand<bool>
{
    /// <summary>
    /// Gets the unique identifier of the client to delete.
    /// </summary>
    public Guid ClientId { get; init; }
}