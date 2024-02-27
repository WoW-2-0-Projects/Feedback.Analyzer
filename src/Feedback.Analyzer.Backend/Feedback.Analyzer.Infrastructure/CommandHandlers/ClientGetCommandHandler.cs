using Feedback.Analyzer.Application.Clients.Commands;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.CommandHandlers;

/// <summary>
/// Command handler for retrieving a collection of clients based on specified criteria.
/// </summary>
public class ClientGetCommandHandler(IClientService clientService) : ICommandHandler<ClientGetCommand, IQueryable<Client>>
{
    public Task<IQueryable<Client>> Handle(ClientGetCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(request.ClientFilter is not null ? clientService.Get(request.ClientFilter, new QueryOptions(){ AsNoTracking = true }) : throw new InvalidOperationException());
    }
}