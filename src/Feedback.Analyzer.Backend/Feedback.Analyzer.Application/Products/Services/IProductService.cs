using System.Linq.Expressions;
using Feedback.Analyzer.Application.Products.Models;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Products.Services;

/// <summary>
/// Defines an interface for services that manage product data.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Retrieves a collection of products optionally filtered by a predicate or a ProductFilter.
    /// </summary>
    /// <param name="predicate">An optional expression used to filter products (null for no filtering).</param>
    /// <param name="queryOptions">Optional options for customizing the query behavior (e.g., sorting, data selection).</param>
    /// <returns>An IQueryable of Product objects representing the filtered product data.</returns>
    IQueryable<Product> Get(Expression<Func<Product, bool>>? predicate = default, QueryOptions queryOptions = default);

    /// <summary>
    /// Retrieves a collection of products filtered by the provided ProductFilter.
    /// </summary>
    /// <param name="productFilter">An instance of ProductFilter containing the filtering criteria.</param>
    /// <param name="queryOptions">Optional options for customizing the query behavior (e.g., sorting, data selection).</param>
    /// <returns>An IQueryable of Product objects representing the filtered product data.</returns>
    IQueryable<Product> Get(ProductFilter productFilter, QueryOptions queryOptions = default);

    /// <summary>
    /// Retrieves a product by its ID asynchronously.
    /// </summary>
    /// <param name="productId">The ID of the product to retrieve.</param>
    /// <param name="queryOptions">Optional options for customizing the query behavior (e.g., specifying related entities).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A ValueTask of Product? representing the retrieved product or null if not found.</returns>
    ValueTask<Product?> GetByIdAsync(Guid productId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new product asynchronously.
    /// </summary>
    /// <param name="product">The product data to be created.</param>
    /// <param name="commandOptions">Optional options for customizing the command behavior (e.g., validation).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A ValueTask of Product representing the created product.</returns>
    ValueTask<Product> CreateAsync(Product product, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing product asynchronously.
    /// </summary>
    /// <param name="product">The product data containing the updated values.</param>
    /// <param name="commandOptions">Optional options for customizing the command behavior (e.g., validation).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A ValueTask of Product representing the updated product.</returns>
    ValueTask<Product> UpdateAsync(Product product, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a product asynchronously.
    /// </summary>
    /// <param name="product">The product to be deleted.</param>
    /// <param name="commandOptions">Optional options for customizing the command behavior (e.g., cascading deletes).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A ValueTask of Product? representing the deleted product or null if deletion fails.</returns>
    ValueTask<Product?> DeleteAsync(Product product, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a product by its ID asynchronously.
    /// </summary>
    /// <param name="productId">The ID of the product to be deleted.</param>
    /// <param name="commandOptions">Optional options for customizing the command behavior (e.g., cascading deletes).</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A ValueTask of Product? representing the deleted product or null if deletion fails.</returns>
    ValueTask<Product?> DeleteByIdAsync(Guid productId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default);
}

