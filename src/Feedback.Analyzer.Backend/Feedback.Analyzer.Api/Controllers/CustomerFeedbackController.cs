using Feedback.Analyzer.Application.CustomerFeedbacks.Commands;
using Feedback.Analyzer.Application.CustomerFeedbacks.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Analyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerFeedbackController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] CustomerFeedbackGetQuery customerFeedbackGetQuery, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(customerFeedbackGetQuery,cancellationToken);

        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("{customerFeedbackId:guid}")]
    public async ValueTask<IActionResult> GetCustomerFeedbackById([FromRoute]Guid customerFeedbackId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CustomerFeedbackGetByIdQuery() { ProductId = customerFeedbackId },
            cancellationToken);
        return result is not null ? Ok(result) : NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomerFeedback([FromBody] CustomerFeedbackCreateCommand command, 
                                                            CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    /*[HttpPut]   We don't use it now 
    public async ValueTask<IActionResult> UpdateCustomerFeedback([FromBody]CustomerFeedbackUpdateCommand customerFeedbackUpdateCommand, 
                                                                 CancellationToken cancellationToken)
    {
        var result = await mediator.Send(customerFeedbackUpdateCommand, cancellationToken);
        return Ok(result);
    }*/

    [HttpDelete("{customerFeedbackId:guid}")]
    public async ValueTask<IActionResult> DeleteCustomerFeedbackById([FromRoute] Guid customerFeedbackId, 
                                                                 CancellationToken cancellationToken = default)
    {
        var result =  await mediator
            .Send(new CustomerFeedbackDeleteByIdCommand(){ ProductId = customerFeedbackId}, cancellationToken);
        return  result ? Ok(result) : BadRequest();
    }
}