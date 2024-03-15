using Feedback.Analyzer.Application.Common.Workflows.Events;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Analyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkflowsController(IMediator mediator) : ControllerBase
{
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