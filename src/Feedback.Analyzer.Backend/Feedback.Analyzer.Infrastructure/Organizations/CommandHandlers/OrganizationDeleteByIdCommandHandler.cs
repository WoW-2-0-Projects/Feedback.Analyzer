using AutoMapper;
using Feedback.Analyzer.Application.Organizations.Commands;
using Feedback.Analyzer.Application.Organizations.Services;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Infrastructure.Organizations.CommandHandlers;

/// <summary>
/// Handles the execution of the <see cref="OrganizationDeleteByIdCommand"/>,
/// responsible for deleting an organization.
/// </summary>
public class OrganizationDeleteByIdCommandHandler(IMapper mapper, IOrganizationService organizationService) : ICommandHandler<OrganizationDeleteByIdCommand, bool>
{
    public async Task<bool> Handle(OrganizationDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        await organizationService.DeleteByIdAsync(request.OrganizationId, cancellationToken: cancellationToken);
        return true;
    }
}