using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

public interface IProductRepository
{
    IQueryable<Product> Get(Expression<Func<Product, bool>>? predicate = default, QueryOptions queryOptions = default);

    ValueTask<Product?> GetByIdAsync(Guid productId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<IList<Product>> GetByIdsAsync(IEnumerable<Guid> ids, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Product> CreateAsync(Product product, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Product> UpdateAsync(Product product, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Product?> DeleteByIdAsync(Guid productId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Product?> DeleteAsync(Product product, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}