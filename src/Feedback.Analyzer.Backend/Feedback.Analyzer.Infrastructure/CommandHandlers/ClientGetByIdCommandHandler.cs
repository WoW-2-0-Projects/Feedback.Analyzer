using Feedback.Analyzer.Application.Clients.Commands;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.CommandHandlers;

public class ClientGetByIdCommandHandler(IClientService clientService) : ICommandHandler<ClientGetByIdCommand, Client?>
{
    public async Task<Client?> Handle(ClientGetByIdCommand request, CancellationToken cancellationToken)
    {
        return await clientService.GetByIdAsync(request.ClientId, new QueryOptions() { AsNoTracking = true }, cancellationToken);
    }
}