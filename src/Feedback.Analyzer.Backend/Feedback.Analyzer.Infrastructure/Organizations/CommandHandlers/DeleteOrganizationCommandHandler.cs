using AutoMapper;
using Feedback.Analyzer.Application.Organizations.Commands;
using Feedback.Analyzer.Application.Organizations.Services;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Infrastructure.Organizations.CommandHandlers;

/// <summary>
/// Handles the execution of the <see cref="DeleteOrganizationByIdCommand"/>,
/// responsible for deleting an organization.
/// </summary>
public class DeleteOrganizationCommandHandler(IMapper mapper, IOrganizationService organizationService) : ICommandHandler<DeleteOrganizationByIdCommand, bool>
{
    public async Task<bool> Handle(DeleteOrganizationByIdCommand request, CancellationToken cancellationToken)
    {
        await organizationService.DeleteByIdAsync(request.OrganizationId, new CommandOptions(), cancellationToken);
        return true;
    }
}