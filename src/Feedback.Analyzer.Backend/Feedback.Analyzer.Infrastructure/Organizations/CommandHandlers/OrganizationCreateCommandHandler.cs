using AutoMapper;
using Feedback.Analyzer.Application.Organizations.Commands;
using Feedback.Analyzer.Application.Organizations.Models;
using Feedback.Analyzer.Application.Organizations.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Organizations.CommandHandlers;

/// <summary>
/// Handles the execution of the <see cref="OrganizationCreateCommand"/>.
/// Responsible for coordinating the creation of a new organization.
/// </summary>
public class OrganizationCreateCommandHandler(IMapper mapper, IOrganizationService organizationService) : ICommandHandler<OrganizationCreateCommand, OrganizationDto>
{
    public async Task<OrganizationDto> Handle(OrganizationCreateCommand request, CancellationToken cancellationToken)
    {
        // Conversion to domain entity cancellationToken
        var organization = mapper.Map<Organization>(request.Organization);
        
        // Call service
        var createdOrganization = await organizationService.CreateAsync(organization, cancellationToken: cancellationToken);

        // Conversion to DTO
        var organizationDto = mapper.Map<OrganizationDto>(createdOrganization);

        return organizationDto;
    }
}