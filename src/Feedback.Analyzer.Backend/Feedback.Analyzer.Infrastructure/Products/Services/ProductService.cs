using System.Linq.Expressions;
using Feedback.Analyzer.Application.Products.Models;
using Feedback.Analyzer.Application.Products.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Infrastructure.Products.Validators;
using Feedback.Analyzer.Persistence.Extensions;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;

namespace Feedback.Analyzer.Infrastructure.Products.Services;

/// <summary>
/// Implements the IProductService interface to manage product data using a product repository and validator.
/// </summary>
/// <param name="productRepository"></param>
/// <param name="productValidator"></param>
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
            return productRepository.Get().ApplyPagination(productFilter);
        }

        public ValueTask<Product?> GetByIdAsync(Guid productId, QueryOptions queryOptions = default, CancellationToken cancellationToken = default)
        {
            return productRepository.GetByIdAsync(productId, queryOptions, cancellationToken);
        }

        public ValueTask<Product> CreateAsync(Product product, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
        {
            var validationResult = productValidator.Validate(product,
                options => options.IncludeRuleSets(EntityEvent.OnCreate.ToString()));

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            return productRepository.CreateAsync(product, commandOptions, cancellationToken);
        }

        public async ValueTask<Product> UpdateAsync(Product product, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
        {
            var foundProduct = await GetByIdAsync(product.Id, cancellationToken: cancellationToken) ?? throw new InvalidOperationException();

            foundProduct.Name = product.Name;
            foundProduct.Description = product.Description;

            return await productRepository.UpdateAsync(foundProduct, commandOptions, cancellationToken);
        }

        public ValueTask<Product?> DeleteAsync(Product product, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
        {
            return productRepository.DeleteAsync(product, commandOptions, cancellationToken);
        }

        public ValueTask<Product?> DeleteByIdAsync(Guid productId, CommandOptions commandOptions = default, CancellationToken cancellationToken = default)
        {
            return productRepository.DeleteByIdAsync(productId, commandOptions, cancellationToken);
        }
}