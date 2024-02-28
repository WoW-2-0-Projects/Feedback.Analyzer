﻿using System.Linq.Expressions;
using Feedback.Analyzer.Application.Organizations;
using Feedback.Analyzer.Application.Organizations.Models;
using Feedback.Analyzer.Application.Organizations.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Infrastructure.Organizations.Validators;
using Feedback.Analyzer.Persistence.Extensions;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;

namespace Feedback.Analyzer.Infrastructure.Organizations.Services;

public class OrganizationService(
    IOrganizationRepository organizationRepository,
    OrganizationValidator organizationValidator ) 
    : IOrganizationService
{
    public IQueryable<Organization> Get(
        Expression<Func<Organization, bool>>? predicate, QueryOptions queryOptions = default) 
        => organizationRepository.Get(predicate, queryOptions);
    
    public IQueryable<Organization> Get(OrganizationFilter organizationFilter, QueryOptions queryOptions = default) 
        => organizationRepository.Get().ApplyPagination(organizationFilter);

    public ValueTask<Organization?> GetByIdAsync(
        Guid organizationId, 
        QueryOptions queryOptions = default, 
        CancellationToken cancellationToken = default
        ) =>
        organizationRepository.GetByIdAsync(organizationId, queryOptions, cancellationToken);
    

    public ValueTask<Organization> CreateAsync(
        Organization organization, 
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var validationResult = organizationValidator
            .Validate(organization,
                options => 
                    options.IncludeRuleSets(EntityEvent.OnCreate.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors.ToString());
        
       return organizationRepository.CreateAsync(organization, commandOptions, cancellationToken);
    }

    public async ValueTask<Organization> UpdateAsync(
        Organization organization,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        var foundOrganization = await GetByIdAsync(organization.Id, cancellationToken: cancellationToken) ?? throw new InvalidOperationException();
        
        foundOrganization.Name = organization.Name;
        foundOrganization.Description = organization.Description;

        return await organizationRepository.UpdateAsync(foundOrganization, commandOptions, cancellationToken);
    }

    public ValueTask<Organization?> DeleteAsync(
        Organization organization,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
        =>
        organizationRepository.DeleteAsync(organization, commandOptions, cancellationToken);

    public ValueTask<Organization?> DeleteByIdAsync(
        Guid organizationId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) 
        =>
        organizationRepository.DeleteByIdAsync(organizationId, commandOptions, cancellationToken);
}