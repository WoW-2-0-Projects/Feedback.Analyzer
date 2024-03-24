using Feedback.Analyzer.Application.FeedbackAnalysisResults.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Analyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResultsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] FeedbackAnalysisResultGetQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("{resultId:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid resultId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new FeedbackAnalysisResultGetByIdQuery(resultId), cancellationToken);
        return result is not null ? Ok(result) : NotFound();
    }
}