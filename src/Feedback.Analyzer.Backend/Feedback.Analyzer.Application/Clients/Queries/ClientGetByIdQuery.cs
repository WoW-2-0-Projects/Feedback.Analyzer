using Feedback.Analyzer.Application.Clients.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Clients.Queries;

/// <summary>
/// Represents a command to retrieve a client by their unique identifier.
/// </summary>
public record ClientGetByIdQuery : IQuery<ClientDto?>
{
    public Guid ClientId { get; init; }
}