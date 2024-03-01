using Feedback.Analyzer.Application.Products.Commands;
using Feedback.Analyzer.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Analyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] ProductGetQuery productGetQuery,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(productGetQuery, cancellationToken);
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("{productId:guid}")]
    public async ValueTask<IActionResult> GetProductById([FromRoute] Guid productId,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new ProductGetByIdQuery { ProductId = productId }, cancellationToken);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost("{organizationId:guid}/products")]
    public async ValueTask<IActionResult> CreateProduct([FromRoute] Guid organizationId ,[FromBody] ProductCreateCommand productCreateCommand,
        CancellationToken cancellationToken = default)
    {
        productCreateCommand.Product.OrganizationId = organizationId;
        var result = await mediator.Send(productCreateCommand, cancellationToken);
        
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateCommand productUpdateCommand,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(productUpdateCommand, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{productId:guid}")]
    public async ValueTask<IActionResult> DeleteProductById([FromRoute] Guid productId,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new ProductDeleteByIdCommand { ProductId = productId }, cancellationToken);
        return result ? Ok() : BadRequest();
    }
}