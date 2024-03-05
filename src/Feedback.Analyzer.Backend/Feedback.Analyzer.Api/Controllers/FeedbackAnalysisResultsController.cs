using AutoMapper;
using Feedback.Analyzer.Application.Common.FeedbackAnalysisResults.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Analyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedbackAnalysisResultsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] FeedbackAnalysisResultGetQuery query,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(query, cancellationToken);

        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("{feedbackAnalysisResultId:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid feedbackAnalysisResultId,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(feedbackAnalysisResultId, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }
}