using System.Linq.Expressions;
using Feedback.Analyzer.Application.Clients.Models;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.Extensions;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;

namespace Feedback.Analyzer.Infrastructure.CustomerFeedbacks.Services;

public class CustomerFeedbackService(ICustomerFeedbackRepository customerFeedbackRepository) : ICustomerFeedbackService
{
    public IQueryable<CustomerFeedback> Get(Expression<Func<CustomerFeedback, bool>>? predicate = default, QueryOptions queryOptions = default) 
        => customerFeedbackRepository.Get(predicate, queryOptions);

    public IQueryable<CustomerFeedback> Get(CustomerFeedbackFilter customerFeedbackFilter, QueryOptions queryOptions = default) 
        => customerFeedbackRepository.Get(queryOptions: queryOptions).ApplyPagination(customerFeedbackFilter);

    public ValueTask<CustomerFeedback?> GetByIdAsync(Guid customerFeedbackId, QueryOptions queryOptions = default,
                                                     CancellationToken cancellationToken = default) 
        => customerFeedbackRepository.GetByIdAsync(customerFeedbackId, queryOptions, cancellationToken);

    public async ValueTask<CustomerFeedback?> UpdateAsync(CustomerFeedback customerFeedback, CommandOptions commandOptions = default,
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