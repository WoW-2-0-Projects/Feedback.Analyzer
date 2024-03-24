﻿using System.Linq.Expressions;
using Feedback.Analyzer.Application.Organizations.Models;
using Feedback.Analyzer.Application.Organizations.Services;
using Feedback.Analyzer.Domain.Brokers;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Infrastructure.Organizations.Validators;
using Feedback.Analyzer.Persistence.Extensions;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Organizations.Services;

/// <summary>
/// Initializes a new instance of the <see cref="OrganizationService"/> class.
/// </summary>
/// <param name="organizationRepository">The repository for managing organization data.</param>
/// <param name="organizationValidator">The validator for ensuring organization data integrity.</param>
public class OrganizationService(
    IOrganizationRepository organizationRepository,
    OrganizationValidator organizationValidator,
    IRequestContextProvider requestContextProvider)
    : IOrganizationService
{
    public IQueryable<Organization> Get(
        Expression<Func<Organization, bool>>? predicate, QueryOptions queryOptions = default)
    {
        if (requestContextProvider.GetUserId() == Guid.Empty)
        {
            throw new UnauthorizedAccessException();
        }

        return organizationRepository.Get(predicate, queryOptions);
    }

    public IQueryable<Organization> Get(OrganizationFilter organizationFilter, QueryOptions queryOptions = default)
    {
        var organizationQuery = organizationRepository
            .Get()
            .ApplyPagination(organizationFilter);

        if (organizationFilter.ClientId.HasValue)
            organizationQuery = organizationQuery
                .Where(organization => organization.ClientId == organizationFilter.ClientId);

        return organizationQuery;
    }

    public async ValueTask<Organization?> GetByIdAsync(
        Guid organizationId,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        var organizationQuery = organizationRepository.Get(organization => organization.Id == organizationId, queryOptions);
        
        if (requestContextProvider.IsLoggedIn()) 
            organizationQuery = organizationQuery
                .Where(organization => organization.ClientId == requestContextProvider.GetUserId());

        return await organizationQuery.FirstOrDefaultAsync(cancellationToken);
    }

    public ValueTask<Organization> CreateAsync(
        Organization organization,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var validationResult = organizationValidator.Validate(organization, options =>
            options.IncludeRuleSets(EntityEvent.OnCreate.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        return organizationRepository.CreateAsync(organization, commandOptions, cancellationToken);
    }

    public async ValueTask<Organization> UpdateAsync(
        Organization organization,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        var validationResult = organizationValidator.Validate(organization, options =>
            options.IncludeRuleSets(EntityEvent.OnUpdate.ToString()));
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var foundOrganization = await GetByIdAsync(organization.Id, cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Organization not found.");

        if (foundOrganization.ClientId != organization.ClientId)
            throw new InvalidOperationException("Can't change organization owner");
        
        foundOrganization!.Name = organization.Name;
        foundOrganization.Description = organization.Description;

        return await organizationRepository.UpdateAsync(foundOrganization, commandOptions, cancellationToken);
    }

    public ValueTask<Organization?> DeleteAsync(
        Organization organization,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
        => organizationRepository.DeleteAsync(organization, commandOptions, cancellationToken);

    public async ValueTask<Organization?> DeleteByIdAsync(
        Guid organizationId,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var foundOrganization = await GetByIdAsync(organizationId, cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Organization not found.");

        return await organizationRepository.DeleteByIdAsync(foundOrganization!.Id, commandOptions, cancellationToken);
    }
}