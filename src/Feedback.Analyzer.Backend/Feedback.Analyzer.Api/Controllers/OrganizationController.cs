using Feedback.Analyzer.Application.Organizations.Commands;
using Feedback.Analyzer.Application.Organizations.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Analyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> GetAsync([FromQuery] OrganizationGetQuery organizationGetQuery, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(organizationGetQuery, cancellationToken);
        return result.Any() ? Ok(result) : NoContent();
    }
    
    [HttpGet("{organizationId:guid}")]
    public async Task<IActionResult> GetOrganizationByIdAsync([FromRoute] Guid organizationId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new OrganizationGetByIdQuery{OrganizationId = organizationId}, cancellationToken);
        return result is not null ? Ok(result) : NoContent();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOrganizationAsync([FromBody] CreateOrganizationCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrganizationAsync([FromBody] UpdateOrganizationCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    
    [HttpDelete("{organizationId:guid}")]
    public async ValueTask<IActionResult> DeleteOrganizationByIdAsync([FromRoute] Guid organizationId, CancellationToken cancellationToken = default)
    {
       var result =  await mediator.Send(new DeleteOrganizationByIdCommand{ OrganizationId = organizationId}, cancellationToken);
        return  result ? Ok() : BadRequest();
    }
}