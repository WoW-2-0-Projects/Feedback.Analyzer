using Feedback.Analyzer.Application.FeedbackAnalysisResults.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Analyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedbackAnalysisResultsController(IMediator mediator) : ControllerBase
{
   [HttpGet]
   public async ValueTask<IActionResult> Get([FromQuery] FeedbackAnalysisResultGetQuery feedbackAnalysisResult, CancellationToken cancellationToken)
   {
      var result = await mediator.Send(feedbackAnalysisResult,cancellationToken);

      return result.Any() ? Ok(result) : NotFound();
   }

   [HttpGet("{workflowId:guid}")]
   public async ValueTask<IActionResult> GetWorkflowResultById([FromRoute] Guid workflowId,
      CancellationToken cancellationToken)
   {
      var result =
         await mediator.Send(new FeedbackAnalysisResultGetByWorkflowResultIdQuery() { WorkflowResultId = workflowId },
            cancellationToken);
      return result is not null ? Ok(result) : NotFound();
   }
}