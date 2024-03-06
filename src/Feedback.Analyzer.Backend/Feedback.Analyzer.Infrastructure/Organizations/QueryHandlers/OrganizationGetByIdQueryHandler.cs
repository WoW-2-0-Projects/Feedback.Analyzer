using AutoMapper;
using Feedback.Analyzer.Application.Organizations.Models;
using Feedback.Analyzer.Application.Organizations.Queries;
using Feedback.Analyzer.Application.Organizations.Services;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Infrastructure.Organizations.QueryHandlers;


/// <summary>
/// Represents a query handler responsible for retrieving an organization by its ID.
/// </summary>
public class OrganizationGetByIdQueryHandler(IOrganizationService organizationService, IMapper mapper) : IQueryHandler<OrganizationGetByIdQuery, OrganizationDto>
{
    public async Task<OrganizationDto> Handle(OrganizationGetByIdQuery request, CancellationToken cancellationToken)
    {
        var foundOrganization = await organizationService.GetByIdAsync(
            request.OrganizationId,
            new QueryOptions()
            {
                AsNoTracking = true
            },
            cancellationToken
        );
        
        return mapper.Map<OrganizationDto>(foundOrganization);
    }
}