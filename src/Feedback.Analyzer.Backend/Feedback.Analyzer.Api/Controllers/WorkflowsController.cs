using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Queries;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Events;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Analyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkflowsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> GetWorkflows([FromQuery] FeedbackWorkflowGetQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("{workflowId:guid}/results")]
    public async ValueTask<IActionResult> GetWorkflowResultsById([FromRoute] Guid workflowId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new FeedbackWorkflowResultGetByWorkflowIdQuery
            {
                WorkflowId = workflowId
            },
            cancellationToken
        );

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

    [HttpPost("{workflowId:guid}/execute")]
    public async ValueTask<IActionResult> ExecuteWorkflowAsync([FromRoute] Guid workflowId, CancellationToken cancellationToken)
    {
        var executeWorkflowEvent = new AnalyzeWorkflowFeedbacksEvent
        {
            WorkflowId = workflowId
        };

        await mediator.Publish(executeWorkflowEvent, cancellationToken);

        return Accepted(executeWorkflowEvent.Id);
    }

    #endregion
}