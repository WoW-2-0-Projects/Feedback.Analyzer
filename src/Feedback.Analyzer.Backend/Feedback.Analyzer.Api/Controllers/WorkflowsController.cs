using Feedback.Analyzer.Application.Common.Workflows.Events;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Queries;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Query;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Commands;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Events;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Analyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkflowsController(IMediator mediator) : ControllerBase
{
    #region Common

    [HttpGet]
    public async ValueTask<IActionResult> Get(
        [FromQuery] FeedbackAnalysisWorkflowGetQuery feedbackAnalysisWorkflowGetQuery,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.Send(feedbackAnalysisWorkflowGetQuery, cancellationToken);
        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("{workflowId:guid}")]
    public async ValueTask<IActionResult> GetFeedbackAnalysisWorkflowById([FromRoute] Guid workflowId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new FeedbackAnalysisWorkflowGetByIdQuery
            {
                Id = workflowId
            },
            cancellationToken
        );
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateFeedbackAnalysisWorkflow(
        [FromBody] FeedbackAnalysisWorkflowCreateCommand feedbackAnalysisWorkflowCreateCommand,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.Send(feedbackAnalysisWorkflowCreateCommand, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateFeedbackAnalysisWorkflow(
        [FromBody] FeedbackAnalysisWorkflowUpdateCommand feedbackAnalysisWorkflowUpdateCommand,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.Send(feedbackAnalysisWorkflowUpdateCommand, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{workflowId:guid}")]
    public async ValueTask<IActionResult> DeleteFeedbackAnalysisWorkflowByIdAsync([FromRoute] Guid workflowId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new FeedbackAnalysisWorkflowDeleteByIdCommand
            {
                Id = workflowId
            },
            cancellationToken
        );
        return result ? Ok() : BadRequest();
    }

    #endregion

    #region Execution

    [HttpPost("{workflowId:guid}/execute/{promptId:guid}")]
    public async ValueTask<IActionResult> ExecutePrompt([FromRoute] Guid workflowId, [FromRoute] Guid promptId, CancellationToken cancellationToken)
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

    #region Results

    [HttpGet("{workflowId:guid}/results")]
    public async ValueTask<IActionResult> GetWorkflowResultsById(
        [FromRoute] Guid workflowId,
        [FromQuery] FeedbackWorkflowResultGetQuery query, 
        CancellationToken cancellationToken)
    {
        query.Filter.WorkflowId = workflowId;
        var result = await mediator.Send(query, cancellationToken);
        return result.Any() ? Ok(result) : NoContent();
    }
    
    [HttpGet("{workflowId:guid}/results/{resultId:guid}/results")]
    public async ValueTask<IActionResult> Get(
        [FromRoute] Guid resultId,
        [FromQuery] FeedbackAnalysisResultGetQuery query, 
        CancellationToken cancellationToken)
    {
        query.Filter.ResultId = resultId;
        var result = await mediator.Send(query ,cancellationToken);
        return result.Any() ? Ok(result) : NoContent();
    }

    #endregion
}