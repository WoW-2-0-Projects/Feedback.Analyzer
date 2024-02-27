using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Clients.Queries;

/// <summary>
/// Represents a command to retrieve a client by their unique identifier.
/// </summary>
public class ClientGetByIdQuery : IQuery<Client>
{
    public Guid ClientId { get; set; }
}