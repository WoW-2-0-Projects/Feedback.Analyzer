using AutoMapper;
using Feedback.Analyzer.Application.Products.Commands;
using Feedback.Analyzer.Application.Products.Models;
using Feedback.Analyzer.Application.Products.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Products.CommandHandlers;

/// <summary>
/// Handles the ProductCreateCommand by creating a new product using the product service and mapping the result to a ProductDto.
/// </summary>
/// <param name="productService"></param>
/// <param name="mapper"></param>
public class ProductCreateCommandHandler(
    IProductService productService, 
    IMapper mapper) : 
    ICommandHandler<ProductCreateCommand, ProductDto>
{
    public async Task<ProductDto> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    { 

        var product = mapper.Map<Product>(request.Product);

        var createdProduct = await productService.CreateAsync(product, cancellationToken: cancellationToken);

        var productDto = mapper.Map<ProductDto>(createdProduct);

        return productDto;
    }
}