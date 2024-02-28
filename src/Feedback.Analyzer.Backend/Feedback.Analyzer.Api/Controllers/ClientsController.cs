using Feedback.Analyzer.Application.Clients.Commands;
using Feedback.Analyzer.Application.Clients.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Analyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] ClientGetQuery clientGetQuery, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(clientGetQuery, cancellationToken);
        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("{clientId:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid clientId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ClientGetByIdQuery{ClientId = clientId}, cancellationToken);
        return result is not null ? Ok(result) : NoContent();
    }

    [HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] ClientUpdateCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{clientId:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid clientId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ClientDeleteByIdCommand {ClientId = clientId}, cancellationToken);
        return result ? Ok() : BadRequest();
    }
}