using AutoMapper;
using Feedback.Analyzer.Application.Products.Models;
using Feedback.Analyzer.Application.Products.Queries;
using Feedback.Analyzer.Application.Products.Services;
using Feedback.Analyzer.Domain.Brokers;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Products.QueryHandlers;

/// <summary>
/// Handles the ProductGetQuery by retrieving a collection of products using the product service and filtering criteria, and mapping the results to a collection of ProductDto.
/// </summary>
public class ProductGetQueryHandler(IMapper mapper, IProductService productService, IRequestContextProvider requestContextProvider)
    : IQueryHandler<ProductGetQuery, ICollection<ProductDto>>
{
    public async Task<ICollection<ProductDto>> Handle(ProductGetQuery request, CancellationToken cancellationToken)
    {
        var clientId = requestContextProvider.GetUserId();
        request.Filter.ClientId = clientId;

        var queryOptions = new QueryOptions { TrackingMode = QueryTrackingMode.AsNoTracking };
        var matchedProductsId = await productService
            .Get(request.Filter, queryOptions)
            .Select(product => product.Id)
            .ToListAsync(cancellationToken);

        var matchedProducts = matchedProductsId.Count > 0
            ? await productService
                .Get(product => matchedProductsId.Contains(product.Id), queryOptions)
                .ToArrayAsync(cancellationToken)
            : Array.Empty<Product>();

        return mapper.Map<ICollection<ProductDto>>(matchedProducts);
    }
}