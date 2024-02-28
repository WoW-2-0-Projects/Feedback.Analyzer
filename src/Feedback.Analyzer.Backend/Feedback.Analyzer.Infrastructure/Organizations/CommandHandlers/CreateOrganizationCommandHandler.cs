using AutoMapper;
using Feedback.Analyzer.Api.Models.DTOs;
using Feedback.Analyzer.Application.Organizations;
using Feedback.Analyzer.Application.Organizations.Commands;
using Feedback.Analyzer.Application.Organizations.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Organizations.CommandHandlers;

/// <summary>
/// Handles the execution of the <see cref="CreateOrganizationCommand"/>.
/// Responsible for coordinating the creation of a new organization.
/// </summary>
public class CreateOrganizationCommandHandler(IMapper mapper, IOrganizationService organizationService) : ICommandHandler<CreateOrganizationCommand, OrganizationDto>
{
    public async Task<OrganizationDto> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
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