using AutoMapper;
using Feedback.Analyzer.Application.Products.Commands;
using Feedback.Analyzer.Application.Products.Services;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Infrastructure.Products.CommandHandlers;

/// <summary>
/// Handles the ProductDeleteByIdCommand by deleting a product using the product service and returning a success indicator.
/// </summary>
/// <param name="productService"></param>
/// <param name="mapper"></param>
public class ProductDeleteByIdCommandHandler(IProductService productService, IMapper mapper) : ICommandHandler<ProductDeleteByIdCommand, bool>
{
    public async Task<bool> Handle(ProductDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        var result = await productService.DeleteByIdAsync(request.ProductId, cancellationToken: cancellationToken);

        return result is not null;
    }
}