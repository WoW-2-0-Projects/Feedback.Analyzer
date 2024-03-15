using Feedback.Analyzer.Application.Clients.Queries;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Clients.QueryHandlers;

/// <summary>
/// Query handler responsible for checking the existence of a user by email address.
/// </summary>
public class CheckUserExistenceQueryHandler(IClientService clientService)
    : IQueryHandler<CheckUserByEmailAddressQuery, string?>
{
    public Task<string?> Handle(CheckUserByEmailAddressQuery request, CancellationToken cancellationToken)
    {
        var clientFirstName = clientService
            .Get(
                client => client.EmailAddress == request.EmailAddress,
                new QueryOptions
                {
                    TrackingMode = QueryTrackingMode.AsNoTracking
                }
            )
            .Select(client => client.FirstName)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return clientFirstName;
    }
}