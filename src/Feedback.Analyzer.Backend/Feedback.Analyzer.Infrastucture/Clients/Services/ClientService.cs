using System.Linq.Expressions;
using Feedback.Analyzer.Application.Clients.Model;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Infrastucture.Common.Validators;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;

namespace Feedback.Analyzer.Infrastucture.Clients.Services;

public class ClientService(
    IClientRepository clientRepository,
    ClientValidator clientValidator) : IClientService
{
    public IQueryable<Client> Get
        (Expression<Func<Client, bool>>? predicate = default, bool asNoTracking = false)
        => clientRepository.Get(predicate, asNoTracking);

    public IQueryable<Client> Get
        (ClientFilter clientFilter, bool asNoTracking = false)
    {
        return clientRepository.Get(asNoTracking: asNoTracking).Skip((int)((clientFilter.PageToken - 1) * clientFilter.PageSize))
            .Take((int)clientFilter.PageSize);
    }
    
    public ValueTask<Client?> GetByIdAsync
        (Guid clientId, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => clientRepository.GetByIdAsync(clientId, asNoTracking, cancellationToken);


    public ValueTask<Client> CreateAsync
        (Client client, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var validationResult = clientValidator.Validate(client, 
            options =>
                        options.IncludeRuleSets(EntityEvent.OnCreate.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return clientRepository.CreateAsync(client, saveChanges, cancellationToken);
    }

    public async ValueTask<Client?> UpdateAsync
        (Client client, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundEntity = await GetByIdAsync(client.Id, cancellationToken: cancellationToken) ??
                          throw new InvalidOperationException();

        foundEntity.FirstName = client.FirstName;
        foundEntity.LastName = client.LastName;
        foundEntity.Email = client.Email;

        return await clientRepository.UpdateAsync(foundEntity, saveChanges, cancellationToken);
    }

    public ValueTask<Client?> DeleteAsync
        (Client client, bool saveChanges, CancellationToken cancellationToken = default)
        => clientRepository.DeleteAsync(client, saveChanges, cancellationToken);

    public ValueTask<Client?> DeleteByIdAsync
        (Guid clientId, bool saveChanges = true, CancellationToken cancellationToken = default)
        => clientRepository.DeleteByIdAsync(clientId, saveChanges, cancellationToken);
}