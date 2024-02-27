using AutoMapper;
using Feedback.Analyzer.Api.Models.DTOs;
using Feedback.Analyzer.Application.Clients.Commands;
using Feedback.Analyzer.Application.Clients.Model;
using Feedback.Analyzer.Application.Clients.Queries;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Domain.Common.Query;
using Feedback.Analyzer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] ClientGetQuery clientGetQuery, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(clientGetQuery, cancellationToken);
        return result.Any() ? Ok(mapper.Map<IEnumerable<ClientDto>>(result)) : NoContent();
    }

    [HttpGet("{clientId:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid clientId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ClientGetByIdQuery(){ClientId = clientId}, cancellationToken);
        return result is not null ? Ok(mapper.Map<ClientDto>(result)) : NoContent();
    }

    [HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] ClientDto clientDto, CancellationToken cancellationToken)
    {
        var client = mapper.Map<Client>(clientDto);
        var result = await mediator.Send(new ClientUpdateCommand() { Client = client }, cancellationToken);
        return result is not null ? Ok(mapper.Map<ClientDto>(result)) : NoContent();
    }

    [HttpDelete("{clientId:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid clientId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ClientDeleteByIdCommand(){ClientId = clientId}, cancellationToken);
        return result is not null ? Ok(mapper.Map<ClientDto>(result)) : NoContent();
    }
}