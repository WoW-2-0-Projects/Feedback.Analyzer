using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Clients.Commands;

public class ClientUpdateCommand : ICommand<Client>
{
    public Client? Client { get; set; }
}