using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Clients.Commands;

/// <summary>
/// Represents a command to delete a client by their unique identifier.
/// </summary>
public record ClientDeleteByIdCommand : ICommand<bool>
{
    public Guid ClientId { get; init; }
}