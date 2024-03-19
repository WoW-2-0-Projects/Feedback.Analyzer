﻿using AutoMapper;
using Feedback.Analyzer.Application.Organizations.Commands;
using Feedback.Analyzer.Application.Organizations.Services;
using Feedback.Analyzer.Domain.Brokers;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Infrastructure.Organizations.CommandHandlers;

/// <summary>
/// Handles the execution of the <see cref="OrganizationDeleteByIdCommand"/>,
/// responsible for deleting an organization.
/// </summary>
public class OrganizationDeleteByIdCommandHandler(
    IMapper mapper,
    IOrganizationService organizationService,
    IRequestContextProvider requestContextProvider) : ICommandHandler<OrganizationDeleteByIdCommand, bool>
{
    public async Task<bool> Handle(OrganizationDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        var clientId= requestContextProvider.GetUserId();

        var isFoundClient = organizationService.Get().Where(organization =>
            organization.ClientId == clientId && organization.Id == request.OrganizationId);
        if (!isFoundClient.Any())
        {
            return false;
        }

        await organizationService.DeleteByIdAsync(request.OrganizationId, cancellationToken: cancellationToken);
        return true;
    }
}