using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Commands;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Analyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedbackAnalysisWorkflowController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] FeedbackAnalysisWorkflowGetQuery feedbackAnalysisWorkflowGetQuery, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(feedbackAnalysisWorkflowGetQuery, cancellationToken);
        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("{feedbackAnalysisWorkflowId:guid}")]
    public async ValueTask<IActionResult> GetFeedbackAnalysisWorkflowById([FromRoute] Guid feedbackAnalysisWorkflowId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new FeedbackAnalysisWorkflowGetByIdQuery() {FeedbackAnalysisWorkflowId = feedbackAnalysisWorkflowId}, cancellationToken);

        return result is not null ? Ok(result) : NoContent();
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateFeedbackAnalysisWorkflow(
        [FromBody] FeedbackAnalysisWorkflowCreateCommand feedbackAnalysisWorkflowCreateCommand,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(feedbackAnalysisWorkflowCreateCommand, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateFeedbackAnalysisWorkflow(
        [FromBody] FeedbackAnalysisWorkflowUpdateCommand feedbackAnalysisWorkflowUpdateCommand,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(feedbackAnalysisWorkflowUpdateCommand, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{feedbackAnalysisWorkflowId:guid}")]
    public async ValueTask<IActionResult> DeleteFeedbackAnalysisWorkflowByIdAsync(
        [FromRoute] Guid feedbackAnalysisWorkflowId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new FeedbackAnalysisWorkflowDeleteByIdCommand {FeedbackAnalysisWorkflowId = feedbackAnalysisWorkflowId}, cancellationToken);
        return result ? Ok() : BadRequest();
    }
}