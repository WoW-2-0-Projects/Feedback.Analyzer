using Feedback.Analyzer.Application.Common.PromptsHistory.Commands;
using Feedback.Analyzer.Application.Common.PromptsHistory.Models;
using Feedback.Analyzer.Application.Common.PromptsHistory.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Analyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PromptsHistoryController(IMediator mediator) : ControllerBase
{
    [HttpGet("{promptId:guid}")]
    public async ValueTask<IActionResult> GetPromptExecutionHistory([FromRoute] Guid promptId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new PromptsHistoryGetByPromptIdQuery() { PromptId = promptId }, cancellationToken);
        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] PromptsHistoryCreateCommand promptsHistoryCreateCommand, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(promptsHistoryCreateCommand, cancellationToken);
        return Ok(result);
    }

}