using AutoMapper;
using Feedback.Analyzer.Application.Products.Models;
using Feedback.Analyzer.Application.Products.Queries;
using Feedback.Analyzer.Application.Products.Services;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Infrastructure.Products.QueryHandlers;

/// <summary>
/// Handles the ProductGetByIdQuery by retrieving a product using the product service and mapping the result to a ProductDto (or null if not found).
/// </summary>
/// <param name="productService"></param>
/// <param name="mapper"></param>
public class ProductGetByIdQueryHandler(IProductService productService, IMapper mapper) : IQueryHandler<ProductGetByIdQuery, ProductDto?>
{
    public async Task<ProductDto?> Handle(ProductGetByIdQuery request, CancellationToken cancellationToken)
    {
        var foundProduct = await productService.GetByIdAsync(request.ProductId, new QueryOptions()
        {
            TrackingMode = QueryTrackingMode.AsNoTracking
        }, cancellationToken
            );

        return mapper.Map<ProductDto>(foundProduct);
    }
}