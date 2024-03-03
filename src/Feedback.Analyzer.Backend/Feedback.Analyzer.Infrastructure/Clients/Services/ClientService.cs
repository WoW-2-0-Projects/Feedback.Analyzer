using System.Linq.Expressions;
using Feedback.Analyzer.Application.Clients.Models;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Persistence.Extensions;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;

namespace Feedback.Analyzer.Infrastructure.Clients.Services;

/// <summary>
/// Service implementation for managing client entities.
/// </summary>
public class ClientService(IClientRepository clientRepository, IValidator<Client> clientValidator) : IClientService
{
    public IQueryable<Client> Get(Expression<Func<Client, bool>>? predicate = default, QueryOptions queryOptions = default)
        => clientRepository.Get(predicate, queryOptions);

    public IQueryable<Client> Get(ClientFilter clientFilter, QueryOptions queryOptions = default)
        => clientRepository.Get(queryOptions: queryOptions).ApplyPagination(clientFilter);

    public ValueTask<Client?> GetByIdAsync(Guid clientId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default)
        => clientRepository.GetByIdAsync(clientId, queryOptions, cancellationToken);

    public ValueTask<Client> CreateAsync(Client client, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
    {
        var validationResult = clientValidator
            .Validate(client,
                options => 
                    options.IncludeRuleSets(EntityEvent.OnCreate.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        return clientRepository.CreateAsync(client, new CommandOptions() { SkipSaveChanges = false }, cancellationToken);
    }

    public async ValueTask<Client?> UpdateAsync(Client client, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
    {
        var foundClient = await GetByIdAsync(client.Id, cancellationToken: cancellationToken) ?? throw new InvalidOperationException();

        foundClient.FirstName = client.FirstName;
        foundClient.LastName = client.LastName;
        foundClient.EmailAddress = client.EmailAddress;

        return await clientRepository.UpdateAsync(foundClient, commandOptions, cancellationToken);
    }
    
    public ValueTask<Client?> DeleteByIdAsync(Guid clientId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
        => clientRepository.DeleteByIdAsync(clientId, commandOptions, cancellationToken);
}