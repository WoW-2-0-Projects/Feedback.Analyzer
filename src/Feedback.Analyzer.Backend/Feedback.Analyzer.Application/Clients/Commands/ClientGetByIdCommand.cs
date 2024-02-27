using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Clients.Commands;

/// <summary>
/// Represents a command to retrieve a client by their unique identifier.
/// </summary>
public class ClientGetByIdCommand : ICommand<Client?>
{
    public Guid ClientId { get; set; }
}