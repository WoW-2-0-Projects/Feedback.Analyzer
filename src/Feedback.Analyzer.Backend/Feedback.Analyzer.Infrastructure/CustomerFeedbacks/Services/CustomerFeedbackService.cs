using System.Linq.Expressions;
using Feedback.Analyzer.Application.CustomerFeedbacks.Models;
using Feedback.Analyzer.Application.CustomerFeedbacks.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Infrastructure.CustomerFeedbacks.Validators;
using Feedback.Analyzer.Persistence.Extensions;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;

namespace Feedback.Analyzer.Infrastructure.CustomerFeedbacks.Services;

/// <summary>
/// Represents a new instance of the <see cref="CustomerFeedbackService"/> class.
/// </summary>
/// <param name="customerFeedbackRepository">The repository for accessing customer feedback data.</param>
/// <param name="customerFeedbackValidator">The validator for validating customer feedback entities.</param>
public class CustomerFeedbackService(ICustomerFeedbackRepository customerFeedbackRepository, CustomerFeedbackValidator customerFeedbackValidator) 
    : ICustomerFeedbackService
{
    public IQueryable<CustomerFeedback> Get(Expression<Func<CustomerFeedback, bool>>? predicate = default, QueryOptions queryOptions = default) 
        => customerFeedbackRepository.Get(predicate, queryOptions);

    public IQueryable<CustomerFeedback> Get(CustomerFeedbackFilter customerFeedbackFilter, 
                                            QueryOptions queryOptions = default) 
        => customerFeedbackRepository.Get(queryOptions: queryOptions).ApplyPagination(customerFeedbackFilter);

    public ValueTask<CustomerFeedback?> GetByIdAsync(Guid customerFeedbackId, QueryOptions queryOptions = default,
                                                     CancellationToken cancellationToken = default) 
        => customerFeedbackRepository.GetByIdAsync(customerFeedbackId, queryOptions, cancellationToken);

    public ValueTask<CustomerFeedback> CreateAsync(CustomerFeedback customerFeedback, 
                                                   CommandOptions commandOptions = default,
                                                   CancellationToken cancellationToken = default)
    {
        var validatorResult = customerFeedbackValidator.Validate
        (
            customerFeedback,
            options => options
                .IncludeRuleSets(EntityEvent.OnCreate.ToString())
        );

        if (!validatorResult.IsValid)
            throw new ValidationException(validatorResult.Errors);
        
        return customerFeedbackRepository.CreateAsync(customerFeedback,commandOptions, cancellationToken);
    }
    
    public async ValueTask<CustomerFeedback?> UpdateAsync(CustomerFeedback customerFeedback, 
                                                          CommandOptions commandOptions = default,
                                                          CancellationToken cancellationToken = default)
    {
        var foundCustomerFeedback =
            await customerFeedbackRepository.GetByIdAsync(customerFeedback.Id, cancellationToken: cancellationToken) 
            ?? throw new InvalidOperationException($"Customer feedback not found {nameof(customerFeedback)}");

        foundCustomerFeedback.ProductId = customerFeedback.ProductId;
        foundCustomerFeedback.Product = customerFeedback.Product;
        foundCustomerFeedback.Comment = customerFeedback.Comment;
        foundCustomerFeedback.UserName = customerFeedback.UserName;

        return await customerFeedbackRepository.UpdateAsync(foundCustomerFeedback, commandOptions, cancellationToken);


    }

    public ValueTask<CustomerFeedback?> DeleteByIdAsync(Guid customerFeedbackId, CommandOptions commandOptions = default,
                                                        CancellationToken cancellationToken = default) 
        => customerFeedbackRepository.DeleteByIdAsync(customerFeedbackId, commandOptions, cancellationToken);
}