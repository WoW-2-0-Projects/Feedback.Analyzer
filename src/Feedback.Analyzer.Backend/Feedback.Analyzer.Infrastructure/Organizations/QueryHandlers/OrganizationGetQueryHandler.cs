using AutoMapper;
using Feedback.Analyzer.Application.Organizations.Models;
using Feedback.Analyzer.Application.Organizations.Queries;
using Feedback.Analyzer.Application.Organizations.Services;
using Feedback.Analyzer.Domain.Brokers;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Organizations.QueryHandlers;

/// <summary>
/// Handles the execution of the <see cref="OrganizationGetByIdQuery"/>.
/// Responsible for retrieving a specific organization based on its ID.
/// </summary>
public class OrganizationGetQueryHandler(
    IMapper mapper,
    IOrganizationService organizationService,
    IRequestContextProvider requestContextProvider) : IQueryHandler<OrganizationGetQuery, ICollection<OrganizationDto>>
{
    public async Task<ICollection<OrganizationDto>> Handle(OrganizationGetQuery organizationGetQuery,
                                                           CancellationToken cancellationToken)
    {
        organizationGetQuery.Filter.ClientId = requestContextProvider.GetUserId();

        var queryOptions = new QueryOptions(QueryTrackingMode.AsNoTracking);

        var matchedOrganizations = await (await organizationService
                                                .Get(organizationGetQuery.Filter, queryOptions)
                                                .GetFilteredEntitiesQuery(organizationService.Get(),
                                                    cancellationToken: cancellationToken))
                                         .ApplyTrackingMode(queryOptions.TrackingMode).ToListAsync(cancellationToken);
        
        return mapper.Map<ICollection<OrganizationDto>>(matchedOrganizations);
    }
}