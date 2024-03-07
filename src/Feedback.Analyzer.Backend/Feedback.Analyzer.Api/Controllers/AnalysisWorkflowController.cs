using Feedback.Analyzer.Application.AnalysisWorkflows.Commands;
using Feedback.Analyzer.Application.AnalysisWorkflows.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Analyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalysisWorkflowController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] AnalysisWorkflowGetQuery analysisWorkflow, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(analysisWorkflow, cancellationToken);
        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("{analysisWorkflowId:guid}")]
    public async ValueTask<IActionResult> GetAnalysisWorkflowById([FromRoute] Guid analysisWorkflowId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new AnalysisWorkflowGetByIdQuery {AnalysisWorkflowId = analysisWorkflowId}, cancellationToken);
        return result is not null ? Ok(result) : NoContent();
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateAnalysisWorkflow([FromBody] AnalysisWorkflowCreateCommand analysisWorkflowCreateCommand , CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(analysisWorkflowCreateCommand, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateAnalysisWorkflow([FromBody] AnalysisWorkflowUpdateCommand analysisWorkflowUpdateCommand, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(analysisWorkflowUpdateCommand, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{analysisWorkflowId:guid}")]
    public async ValueTask<IActionResult> DeleteAnalysisWorkflowById([FromRoute] Guid analysisWorkflowId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new AnalysisWorkflowDeleteByIdCommand {AnalysisWorkflowId = analysisWorkflowId}, cancellationToken);
        return result ? Ok() : BadRequest();
    }
 }