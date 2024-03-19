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
    public async ValueTask<IActionResult> Get([FromQuery] OrganizationGetQuery organizationGetQuery, CancellationToken cancellationToken)
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
    public async Task<IActionResult> CreateOrganization([FromBody] OrganizationCreateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result is not null ? Ok(result) : BadRequest();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrganization([FromBody] OrganizationUpdateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    
    [HttpDelete("{organizationId:guid}")]
    public async ValueTask<IActionResult> DeleteOrganizationById([FromRoute] Guid organizationId, CancellationToken cancellationToken = default)
    {
       var result =  await mediator.Send(new OrganizationDeleteByIdCommand{ OrganizationId = organizationId}, cancellationToken);
        return  result ? Ok() : BadRequest();
    }
}