using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Application.Common.EventBus.Brokers;
using Feedback.Analyzer.Application.Common.Identity.Events;
using Feedback.Analyzer.Application.Common.Identity.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Common.Identity.Services;

public class AccountService(IEventBusBroker eventBusBroker, IClientService clientService, IClientRepository clientRepository) : IAccountService
{
    public async ValueTask<Client?> GetUserByEmailAddressAsync(
        string emailAddress,
        QueryTrackingMode trackingMode = QueryTrackingMode.AsTracking,
        CancellationToken cancellationToken = default
        )
    {
        return await clientRepository
            .Get(queryOptions: new QueryOptions{ TrackingMode = trackingMode})
            .FirstOrDefaultAsync(client => client.EmailAddress == emailAddress, cancellationToken: cancellationToken);
    }

    public async ValueTask<Client> CreateClientAsync(Client client, CancellationToken cancellationToken = default)
    {
        var createdClient = await clientService.CreateAsync(client, cancellationToken: cancellationToken);

        var clientCreatedEvent = new ClientCreatedEvent(createdClient);
        await eventBusBroker.PublishLocalAsync(clientCreatedEvent);

        return createdClient;
    }
}