using AutoMapper;
using Feedback.Analyzer.Application.Products.Models;
using Feedback.Analyzer.Application.Products.Queries;
using Feedback.Analyzer.Application.Products.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Products.QueryHandlers;

/// <summary>
/// Handles the ProductGetQuery by retrieving a collection of products using the product service and filtering criteria, and mapping the results to a collection of ProductDto.
/// </summary>
/// <param name="productService"></param>
/// <param name="mapper"></param>
public class ProductGetQueryHandler(IProductService productService, IMapper mapper) : IQueryHandler<ProductGetQuery, ICollection<ProductDto>>
{
    public async Task<ICollection<ProductDto>> Handle(ProductGetQuery request, CancellationToken cancellationToken)
    {
        var matchedProducts = await productService
            .Get(request.Filter, new QueryOptions() { TrackingMode = QueryTrackingMode.AsNoTracking }).ToListAsync(cancellationToken);

        return mapper.Map<ICollection<ProductDto>>(matchedProducts);
    }
}