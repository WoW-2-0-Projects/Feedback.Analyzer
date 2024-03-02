using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Caching.Brokers;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Represents a repository for managing product data.
/// </summary>
public class ProductRepository
    (AppDbContext dbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<Product, AppDbContext>(dbContext, cacheBroker), IProductRepository
{
    public new IQueryable<Product> Get(Expression<Func<Product, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        return base.Get(predicate, queryOptions);
    }

    public new ValueTask<Product?> GetByIdAsync(Guid productId, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(productId, queryOptions, cancellationToken);
    }

    public new ValueTask<IList<Product>> GetByIdsAsync(IEnumerable<Guid> ids, QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.GetByIdsAsync(ids, queryOptions, cancellationToken);
    }

    public new ValueTask<Product> CreateAsync(Product product, CommandOptions commandOptions = default, 
        CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(product, commandOptions, cancellationToken);
    }

    public new ValueTask<Product> UpdateAsync(Product product, CommandOptions commandOptions = default, 
        CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(product, commandOptions, cancellationToken);
    }

    public new ValueTask<Product?> DeleteByIdAsync(Guid productId, CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(productId, commandOptions, cancellationToken);
    }

    public new ValueTask<Product?> DeleteAsync(Product product, CommandOptions commandOptions = default, 
        CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(product, commandOptions, cancellationToken);
    }
}