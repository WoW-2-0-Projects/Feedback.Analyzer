using AutoMapper;
using Feedback.Analyzer.Api.Models.DTOs;
using Feedback.Analyzer.Application.Organizations;
using Feedback.Analyzer.Application.Organizations.Commands;
using Feedback.Analyzer.Application.Organizations.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Organizations.CommandHandlers;

/// <summary>
/// Handles the execution of the <see cref="UpdateOrganizationCommand"/>. Responsible 
/// for coordinating the modification of an existing organization.
/// </summary>
public class UpdateOrganizationCommandHandler(IMapper mapper, IOrganizationService organizationService) : ICommandHandler<UpdateOrganizationCommand, OrganizationDto>
{
    public async Task<OrganizationDto> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
    {
        // Conversion to domain entity cancellationToken
        var organization = mapper.Map<Organization>(request.Organization);
        
        // Call service
        var updatedOrganization = await organizationService.UpdateAsync(organization, new CommandOptions(), cancellationToken);

        // Conversion to DTO
        var organizationDto = mapper.Map<OrganizationDto>(updatedOrganization);

        return organizationDto;
    }
}