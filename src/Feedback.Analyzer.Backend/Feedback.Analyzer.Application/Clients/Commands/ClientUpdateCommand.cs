using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Clients.Commands;

/// <summary>
/// Represents a command to update a client entity.
/// </summary>
public class ClientUpdateCommand : ICommand<Client>
{
    public Client Client { get; set; } = default!;
}