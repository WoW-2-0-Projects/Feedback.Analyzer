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
/// Handles the execution of the <see cref="CustomerFeedbackCreateCommandHandler"/>.
/// Responsible for coordinating the creation of a new organization.
/// </summary>
public class CustomerFeedbackCreateCommandHandler(IMapper mapper, ICustomerFeedbackService customerFeedbackService) 
    : ICommandHandler<CustomerFeedbackCreateCommand, CustomerFeedbackDto>
{
    public async Task<CustomerFeedbackDto> Handle(CustomerFeedbackCreateCommand request, CancellationToken cancellationToken)
    {
        // Conversion to domain entity cancellationToken
        var customerFeedback = mapper.Map<CustomerFeedback>(request.CustomerFeedback);
        
        // Call service
        var createCustomerFeedback =
            await customerFeedbackService.CreateAsync(customerFeedback, cancellationToken: cancellationToken);

        // Conversion to DTO
        var customerFeedbackDto = mapper.Map<CustomerFeedbackDto>(createCustomerFeedback);
        
        return customerFeedbackDto;
    }
    
}