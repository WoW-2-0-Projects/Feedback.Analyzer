using System.Collections.Immutable;
using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Entities;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Represents a base repository for entities with common CRUD operations.
/// </summary>
/// <param name="dbContext"></param>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TContext"></typeparam>
public abstract class EntityRepositoryBase<TEntity, TContext>(TContext dbContext) where TEntity : class, IEntity where TContext : DbContext
{
    protected TContext DbContext => dbContext;

    /// <summary>
    /// Retrieves entities from the repository based on optional filtering conditions and tracking preferences.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="queryOptions"></param>
    /// <returns>An IQueryable,TEntity, representing the query, allowing for further chaining and filtering</returns>
    protected IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = default, QueryOptions queryOptions = default)
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => true);

        if (predicate is not null)
            initialQuery = initialQuery.Where(predicate);

        if (queryOptions.AsNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        return initialQuery;
    }

    /// <summary>
    /// Asynchronously retrieves an entity from the repository by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="queryOptions"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A ValueTask,TEntity,representing the asynchronous operation. The result will be the found entity, or null if not found.</returns>
    protected async ValueTask<TEntity?> GetByIdAsync(Guid id, QueryOptions queryOptions = default, CancellationToken cancellationToken = default)
    {
        var initialQuery = DbContext.Set<TEntity>().AsQueryable();

        if (queryOptions.AsNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        var foundEntity = await initialQuery.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

        return foundEntity;
    }

    /// <summary>
    /// Asynchronously retrieves entities from the repository by a collection of IDs.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="queryOptions"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A ValueTask,IList,TEntity, representing the asynchronous operation. The result will be a list of the found entities</returns>
    protected async ValueTask<IList<TEntity>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => ids.Contains(entity.Id));

        if (queryOptions.AsNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        return await initialQuery.ToListAsync(cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Asynchronously creates a new entity in the repository.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="commandOptions"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A ValueTask,TEntity,representing the asynchronous operation. The result will be the newly created entity.</returns>
    protected async ValueTask<TEntity> CreateAsync(
        TEntity entity,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        if (!commandOptions.SkipSaveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Asynchronously updates a new entity in the repository.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="commandOptions"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A ValueTask,TEntity, representing the asynchronous operation. The result will be the updated entity.</returns>
    protected async ValueTask<TEntity> UpdateAsync(
        TEntity entity,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        DbContext.Set<TEntity>().Update(entity);

        if (!commandOptions.SkipSaveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Batch updates entities matching given predicate using the provided property selectors and value selectors.
    /// </summary>
    /// <param name="batchUpdatePredicate">Predicate to select entities for batch update</param>
    /// <param name="setPropertyCalls">Batch update value selectors</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Number of updated rows.</returns>
    protected async ValueTask<int> UpdateBatchAsync(
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls,
        // IImmutableList<(Func<TEntity, object> propertySelector, Func<TEntity, object> valueSelector)> setPropertyExecutors,
        Expression<Func<TEntity, bool>>? batchUpdatePredicate = default,
        CancellationToken cancellationToken = default
    )
    {
        var entities = DbContext.Set<TEntity>().AsQueryable();

        if (batchUpdatePredicate is not null)
            entities = entities.Where(batchUpdatePredicate);

        return await entities.ExecuteUpdateAsync(setPropertyCalls,
            // categoryPropertySelector => categoryPropertySelector.MapToSetPropertyCalls(setPropertyExecutors),
            cancellationToken
        );
    }

    /// <summary>
    /// Asynchronously deletes a new entity in the repository.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="commandOptions"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A ValueTask,TEntity, representing the asynchronous operation. The result will be the deleted entity, or null if not found.</returns>
    protected async ValueTask<TEntity?> DeleteAsync(
        TEntity entity,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        DbContext.Set<TEntity>().Remove(entity);

        if (!commandOptions.SkipSaveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Asynchronously deletes an existing entity from the repository by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="commandOptions"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A ValueTask,TEntity, representing the asynchronous operation. The result will be the deleted entity, or null if not found.</returns>
    protected async ValueTask<TEntity?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        var entity = await DbContext.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken) ??
                     throw new InvalidOperationException();

        DbContext.Set<TEntity>().Remove(entity);

        if (!commandOptions.SkipSaveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }
}