using AutoMapper;
using Feedback.Analyzer.Application.CustomerFeedbacks.Models;
using Feedback.Analyzer.Application.CustomerFeedbacks.Queries;
using Feedback.Analyzer.Application.CustomerFeedbacks.Services;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Infrastructure.CustomerFeedbacks.QueryHandlers;

/// <summary>
/// Handles the query to retrieve customer feedback by its ID.
/// </summary>
public class CustomerFeedbackGetByIdQueryHandler(ICustomerFeedbackService customerFeedbackService, IMapper mapper) 
    : IQueryHandler<CustomerFeedbackGetByIdQuery, CustomerFeedbackDto?>
{
    public async Task<CustomerFeedbackDto?> Handle(CustomerFeedbackGetByIdQuery request, CancellationToken cancellationToken)
    {
        var foundCustomerFeedback = await customerFeedbackService.GetByIdAsync
        (
            request.ProductId,
            new QueryOptions()
            {
                AsNoTracking = true
            },
            cancellationToken
        );

        return mapper.Map<CustomerFeedbackDto>(foundCustomerFeedback);
    }
    
}