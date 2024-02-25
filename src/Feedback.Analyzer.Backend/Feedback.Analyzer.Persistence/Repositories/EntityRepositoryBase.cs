﻿using System.Linq.Expressions;
using Feedback.Analyzer.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Represents a base repository for entities with common CRUD operations.
/// </summary>
/// <param name="dbContext"></param>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TContext"></typeparam>
public abstract class EntityRepositoryBase<TEntity, TContext>(
    TContext dbContext)
    where TEntity : class, IEntity where TContext : DbContext
{
    protected TContext DbContext => dbContext;

    /// <summary>
    /// Retrieves entities from the repository based on optional filtering conditions and tracking preferences.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="asNoTracking"></param>
    /// <returns></returns>
    protected IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = default, bool asNoTracking = false)
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => true);

        if (predicate is not null)
            initialQuery = initialQuery.Where(predicate);

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        return initialQuery;
    }

    /// <summary>
    /// Asynchronously retrieves an entity from the repository by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected async ValueTask<TEntity?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        var initialQuery = DbContext.Set<TEntity>().AsQueryable();

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        var foundEntity = await initialQuery.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

        return foundEntity;
    }

    /// <summary>
    /// Asynchronously retrieves entities from the repository by a collection of IDs.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected async ValueTask<IList<TEntity>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => ids.Contains(entity.Id));

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        return await initialQuery.ToListAsync(cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Asynchronously creates a new entity in the repository.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected async ValueTask<TEntity> CreateAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Asynchronously updates a new entity in the repository.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected async ValueTask<TEntity> UpdateAsync(
        TEntity entity,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    )
    {
        DbContext.Set<TEntity>().Update(entity);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Asynchronously deletes a new entity in the repository.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected async ValueTask<TEntity?> DeleteAsync(
        TEntity entity,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    )
    {
        DbContext.Set<TEntity>().Remove(entity);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Asynchronously deletes an existing entity from the repository by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    protected async ValueTask<TEntity?> DeleteByIdAsync(
        Guid id,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    )
    {
        var entity = await DbContext.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken) ??
                     throw new InvalidOperationException();

        DbContext.Set<TEntity>().Remove(entity);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }
}