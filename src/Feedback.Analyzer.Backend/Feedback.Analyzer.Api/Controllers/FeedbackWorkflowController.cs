using Feedback.Analyzer.Application.FeedbackWorkflows.Commands;
using Feedback.Analyzer.Application.FeedbackWorkflows.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Analyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedbackWorkflowController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] FeedbackWorkflowGetQuery feedbackWorkflowGetQuery,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(feedbackWorkflowGetQuery, cancellationToken);

        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("{feedbackWorkflowId:guid}")]
    public async ValueTask<IActionResult> GetFeedbackWorkflowById(
        [FromRoute] Guid feedbackWorkflowId,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new FeedbackWorkflowGetByIdQuery { FeedbackWorkflowId = feedbackWorkflowId }, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateFeedbackWorkflow(
        [FromBody] FeedbackWorkflowCreateCommand feedbackWorkflowCreateCommand,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(feedbackWorkflowCreateCommand, cancellationToken);

        return Ok(result);
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateFeedbackWorkflow(
        [FromBody]  FeedbackWorkflowUpdateCommand feedbackWorkflowUpdateCommand,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(feedbackWorkflowUpdateCommand, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{feedbackWorkflowId}")]
    public async ValueTask<IActionResult> DeleteFeedbackWorkflow(
        [FromRoute] Guid feedbackWorkflowId,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new FeedbackWorkflowDeleteByIdCommand() { FeedbackWorkflowId = feedbackWorkflowId }, cancellationToken);
        return result ? Ok() : BadRequest();
    }
}