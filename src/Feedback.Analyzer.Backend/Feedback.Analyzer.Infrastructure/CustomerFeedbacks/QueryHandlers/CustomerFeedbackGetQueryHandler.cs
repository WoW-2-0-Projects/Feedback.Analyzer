using AutoMapper;
using Feedback.Analyzer.Application.CustomerFeedbacks.Models;
using Feedback.Analyzer.Application.CustomerFeedbacks.Queries;
using Feedback.Analyzer.Application.CustomerFeedbacks.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.CustomerFeedbacks.QueryHandlers;

/// <summary>
/// Handles the query to retrieve a collection of customer feedback based on a filter.
/// </summary>
public class CustomerFeedbackGetQueryHandler(ICustomerFeedbackService customerFeedbackService, IMapper mapper)
    : IQueryHandler<CustomerFeedbackGetQuery, ICollection<CustomerFeedbackDto>>
{
    public async Task<ICollection<CustomerFeedbackDto>> Handle(CustomerFeedbackGetQuery request, CancellationToken cancellationToken)
    {
        var matchedCustomerFeedback = 
            await customerFeedbackService
                  .Get(request.Filter, 
                      new QueryOptions() { AsNoTracking = true }).ToListAsync(cancellationToken);
        
        return mapper.Map<ICollection<CustomerFeedbackDto>>(matchedCustomerFeedback);
    }
}