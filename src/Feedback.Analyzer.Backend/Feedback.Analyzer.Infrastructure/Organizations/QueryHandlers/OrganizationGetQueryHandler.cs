using AutoMapper;
using Feedback.Analyzer.Application.Organizations.Models;
using Feedback.Analyzer.Application.Organizations.Queries;
using Feedback.Analyzer.Application.Organizations.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Organizations.QueryHandlers;

/// <summary>
/// Handles the execution of the <see cref="OrganizationGetByIdQuery"/>.
/// Responsible for retrieving a specific organization based on its ID.
/// </summary>
public class OrganizationGetQueryHandler(IOrganizationService organizationService, IMapper mapper) : IQueryHandler<OrganizationGetQuery, ICollection<OrganizationDto>>
{
    public async Task<ICollection<OrganizationDto>> Handle(OrganizationGetQuery organizationGetQuery, CancellationToken cancellationToken)
    {
        var matchedOrganizations =  await organizationService.Get(organizationGetQuery.Filter, new QueryOptions() { AsNoTracking = true }).ToListAsync(cancellationToken);
        
        return mapper.Map<ICollection<OrganizationDto>>(matchedOrganizations);
    }
}