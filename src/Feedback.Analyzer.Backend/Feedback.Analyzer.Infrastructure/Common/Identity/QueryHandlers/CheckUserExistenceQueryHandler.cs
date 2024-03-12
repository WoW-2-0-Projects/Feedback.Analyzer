using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Application.Common.Identity.Queries;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Common.Identity.QueryHandlers;

/// <summary>
/// Provides a handler for the check user by email address query
/// </summary>
public class CheckUserExistenceQueryHandler(IClientService userService)
    : IQueryHandler<CheckClientByEmailAddressQuery, string?>
{
    public Task<string?> Handle(CheckClientByEmailAddressQuery request, CancellationToken cancellationToken)
    {
        var clientFirstname =  userService
            .Get(
                user => user.EmailAddress == request.EmailAddress,
                new QueryOptions
                {
                    AsNoTracking = true
                }
            )
            .Select(user => user.FirstName)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return clientFirstname;
    }
}