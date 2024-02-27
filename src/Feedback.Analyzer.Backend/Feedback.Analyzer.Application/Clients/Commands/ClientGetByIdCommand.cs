using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Clients.Commands;

public class ClientGetByIdCommand : ICommand<Client?>
{
    public Guid ClientId { get; set; }
}