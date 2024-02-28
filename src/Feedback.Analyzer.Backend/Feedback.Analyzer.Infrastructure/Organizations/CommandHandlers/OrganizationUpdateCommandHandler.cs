using AutoMapper;
using Feedback.Analyzer.Application.Organizations.Commands;
using Feedback.Analyzer.Application.Organizations.Models;
using Feedback.Analyzer.Application.Organizations.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Organizations.CommandHandlers;

/// <summary>
/// Handles the execution of the <see cref="OrganizationUpdateCommand"/>. Responsible 
/// for coordinating the modification of an existing organization.
/// </summary>
public class OrganizationUpdateCommandHandler(IMapper mapper, IOrganizationService organizationService) : ICommandHandler<OrganizationUpdateCommand, OrganizationDto>
{
    public async Task<OrganizationDto> Handle(OrganizationUpdateCommand request, CancellationToken cancellationToken)
    {
        // Conversion to domain entity cancellationToken
        var organization = mapper.Map<Organization>(request.Organization);
        
        // Call service
        var updatedOrganization = await organizationService.UpdateAsync(organization, cancellationToken: cancellationToken);

        // Conversion to DTO
        var organizationDto = mapper.Map<OrganizationDto>(updatedOrganization);

        return organizationDto;
    }
}