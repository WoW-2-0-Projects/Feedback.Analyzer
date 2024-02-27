using Feedback.Analyzer.Application.Clients.Commands;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.CommandHandlers;

public class ClientUpdateCommandHandler(IClientService clientService) : ICommandHandler<ClientUpdateCommand, Client?>
{
    public async Task<Client?> Handle(ClientUpdateCommand request, CancellationToken cancellationToken)
    {
        return await clientService.UpdateAsync(request.Client!, new CommandOptions(), cancellationToken);
    }
}