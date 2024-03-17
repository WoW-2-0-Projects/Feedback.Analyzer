using System.Security.Authentication;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Application.Common.Identity.Queries;
using Feedback.Analyzer.Domain.Brokers;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using MediatR;

namespace Feedback.Analyzer.Infrastructure.Common.Identity.QueryHandlers;

public class GetCurrentUserQueryHandler(IRequestContextProvider requestContextProvider, IClientService clientService) 
    : IQueryHandler<GetCurrentUserQuery, Client>
{
    public async Task<Client> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var userId = requestContextProvider.GetUserId();

        var foundUser = await clientService.GetByIdAsync(userId, cancellationToken: cancellationToken);
        
        if(foundUser is null)
            throw new AuthenticationException("Current logged in user not found");
        
        return foundUser;
    }
}