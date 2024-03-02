using AutoMapper;
using Feedback.Analyzer.Application.Products.Commands;
using Feedback.Analyzer.Application.Products.Models;
using Feedback.Analyzer.Application.Products.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Products.CommandHandlers;

/// <summary>
/// Handles the ProductUpdateCommand by updating a product using the product service and mapping the result to a ProductDto.
/// </summary>
/// <param name="productService"></param>
/// <param name="mapper"></param>
public class ProductUpdateCommandHandler(IProductService productService, IMapper mapper) : ICommandHandler<ProductUpdateCommand, ProductDto>
{
    public async Task<ProductDto> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Product>(request.Product);

        var updatedProduct = await productService.UpdateAsync(product, cancellationToken: cancellationToken);

        var productDto = mapper.Map<ProductDto>(updatedProduct);

        return productDto;
    }
}