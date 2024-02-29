using AutoMapper;
using Feedback.Analyzer.Application.Clients.Commands;
using Feedback.Analyzer.Application.Clients.Models;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Application.CustomerFeedbacks.Commands;
using Feedback.Analyzer.Application.CustomerFeedbacks.Models;
using Feedback.Analyzer.Application.CustomerFeedbacks.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.CustomerFeedbacks.CommandHandlers;

/// <summary>
/// Handles the command to update customer feedback.
/// </summary>
public class CustomerFeedbackUpdateCommandHandler(ICustomerFeedbackService customerFeedbackService,IMapper mapper) 
    : ICommandHandler<CustomerFeedbackUpdateCommand, CustomerFeedbackDto>
{
    public async Task<CustomerFeedbackDto> Handle(CustomerFeedbackUpdateCommand request, CancellationToken cancellationToken)
    {
        var customerFeedback = mapper.Map<CustomerFeedback>(request.CustomerFeedback);
        var updateCustomerFeedback = await customerFeedbackService
            .UpdateAsync(customerFeedback,new CommandOptions(),cancellationToken);

        return mapper.Map<CustomerFeedbackDto>(updateCustomerFeedback);
    }
}