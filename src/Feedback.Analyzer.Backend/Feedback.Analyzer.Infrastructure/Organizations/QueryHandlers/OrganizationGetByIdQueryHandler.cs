using AutoMapper;
using Feedback.Analyzer.Api.Models.DTOs;
using Feedback.Analyzer.Application.Organizations.Queries;
using Feedback.Analyzer.Application.Organizations.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Organizations.QueryHandlers;

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