using Feedback.Analyzer.Application.Organizations.Commands;
using Feedback.Analyzer.Application.Organizations.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Analyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> GetAsync([FromQuery] OrganizationGetQuery organizationGetQuery, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(organizationGetQuery, cancellationToken);
        return result.Any() ? Ok(result) : NoContent();
    }
    
    [HttpGet("{organizationId:guid}")]
    public async Task<IActionResult> GetOrganizationById([FromRoute] Guid organizationId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new OrganizationGetByIdQuery{OrganizationId = organizationId}, cancellationToken);
        return result is not null ? Ok(result) : NoContent();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrganization([FromBody] UpdateOrganizationCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    
    [HttpDelete("{organizationId:guid}")]
    public async ValueTask<IActionResult> DeleteOrganizationById([FromRoute] Guid organizationId, CancellationToken cancellationToken = default)
    {
       var result =  await mediator.Send(new DeleteOrganizationByIdCommand{ OrganizationId = organizationId}, cancellationToken);
        return  result ? Ok() : BadRequest();
    }
}