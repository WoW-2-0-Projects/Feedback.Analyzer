using AutoMapper;
using Feedback.Analyzer.Application.Products.Models;
using Feedback.Analyzer.Application.Products.Queries;
using Feedback.Analyzer.Application.Products.Services;
using Feedback.Analyzer.Domain.Brokers;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Extensions;
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
        request.Filter.ClientId = requestContextProvider.GetUserId();
        var queryOptions = new QueryOptions(QueryTrackingMode.AsNoTracking);
        
        var matchedProducts = await (await productService
            .Get(request.Filter, queryOptions)
            .GetFilteredEntitiesQuery(productService.Get(), cancellationToken: cancellationToken))
            .Include(product => product.Organization)
            .ApplyTrackingMode(queryOptions.TrackingMode)
            .ToListAsync(cancellationToken: cancellationToken);

        return mapper.Map<ICollection<ProductDto>>(matchedProducts);
    }
}