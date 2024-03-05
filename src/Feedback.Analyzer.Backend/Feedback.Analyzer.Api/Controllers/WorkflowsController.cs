using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Application.Common.Workflows.Events;
using Feedback.Analyzer.Application.Common.Workflows.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Analyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkflowsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> GetWorkflows([FromQuery] WorkflowGetQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return result.Any() ? Ok(result) : NotFound();
    }

    #region Execution

    [HttpPost("{workflowId:guid}/execute/{promptId:guid}")]
    public async ValueTask<IActionResult> GetPromptResultById(
        [FromRoute] Guid workflowId,
        [FromRoute] Guid promptId,
        CancellationToken cancellationToken
    )
    {
        var executePromptEvent = new ExecuteWorkflowSinglePromptEvent
        {
            PromptId = promptId,
            WorkflowId = workflowId
        };

        await mediator.Publish(executePromptEvent, cancellationToken);
        return Accepted(executePromptEvent.Id);
    }

    #endregion
}