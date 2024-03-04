using Feedback.Analyzer.Application.Common.Prompts.Commands;
using Feedback.Analyzer.Application.PromptsHistory.Queries;
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
        var result = await mediator.Send(new PromptExecutionHistoryGetByPromptIdQuery() { PromptId = promptId }, cancellationToken);
        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] PromptCreateCommand promptsHistoryCreateCommand, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(promptsHistoryCreateCommand, cancellationToken);
        return Ok(result);
    }

}