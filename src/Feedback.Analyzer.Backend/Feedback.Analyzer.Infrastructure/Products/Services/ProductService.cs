using System.Linq.Expressions;
using Feedback.Analyzer.Application.Products.Models;
using Feedback.Analyzer.Application.Products.Services;
using Feedback.Analyzer.Domain.Brokers;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Infrastructure.Products.Validators;
using Feedback.Analyzer.Persistence.Extensions;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Products.Services;

/// <summary>
/// Implements the IProductService interface to manage product data using a product repository and validator.
/// </summary>
public class ProductService
    (IProductRepository productRepository,
        ProductValidator productValidator)
    : IProductService
{
        public IQueryable<Product> Get(Expression<Func<Product, bool>>? predicate, QueryOptions queryOptions = default)
        {
            return productRepository.Get(predicate, queryOptions);
        }

        public IQueryable<Product> Get(ProductFilter productFilter, QueryOptions queryOptions = default)
        {
            var productsQuery = productRepository.Get().ApplyPagination(productFilter);
            
            if(productFilter.ClientId.HasValue)
                productsQuery = productsQuery
                    .Include(product => product.Organization)
                    .Where(product => product.Organization.ClientId == productFilter.ClientId.Value);
            
            return productsQuery;
        }

        public ValueTask<Product?> GetByIdAsync(Guid productId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default)
        {
            return productRepository.GetByIdAsync(productId, queryOptions, cancellationToken);
        }

        public async ValueTask<Product> CreateAsync(Product product, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
        {
            var validationResult = productValidator.Validate(product,
                    options => options.IncludeRuleSets(EntityEvent.OnCreate.ToString()));

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            return await productRepository.CreateAsync(product, commandOptions, cancellationToken);
        }

        public async ValueTask<Product> UpdateAsync(Product product, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
        {
        var validationResult = productValidator.Validate(product,
                options => options.IncludeRuleSets(EntityEvent.OnUpdate.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var fountProduct = await GetByIdAsync(product.Id, cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Product not fount");

        if (await GetCurrentClientId(fountProduct.OrganizationId) == await GetCurrentClientId(product.OrganizationId))
            throw new UnauthorizedAccessException("Client id must match the current user id");

        return await productRepository.UpdateAsync(product, commandOptions, cancellationToken);
    }

        public async ValueTask<Product?> DeleteAsync(Product product, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
        {
        var fountProduct = await GetByIdAsync(product.Id, cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Product not fount");

        if (await GetCurrentClientId(fountProduct.OrganizationId) == await GetCurrentClientId(product.OrganizationId))
            throw new UnauthorizedAccessException("Client id must match the current user id");

        return await productRepository.DeleteAsync(product, commandOptions, cancellationToken);
        }

        public async ValueTask<Product?> DeleteByIdAsync(Guid productId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
        {
        var fountProduct = await GetByIdAsync(productId, cancellationToken: cancellationToken)
                ?? throw new InvalidOperationException("Product not fount");
        return await productRepository.DeleteByIdAsync(productId, commandOptions, cancellationToken);
    }

    private async ValueTask<Guid> GetCurrentClientId(Guid orgnaziationId) 
    {
        var product = await productRepository.Get(product => product.OrganizationId == orgnaziationId).Include(product => product.Organization).FirstOrDefaultAsync();

        return product.Organization.ClientId;
    }
}