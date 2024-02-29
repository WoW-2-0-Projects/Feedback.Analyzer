using Feedback.Analyzer.Application.Clients.Commands;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Application.CustomerFeedbacks.Commands;
using Feedback.Analyzer.Application.CustomerFeedbacks.Services;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Infrastructure.CustomerFeedbacks.CommandHandlers;

/// <summary>
/// Handles the command to delete customer feedback by its ID.
/// </summary>
public class CustomerFeedbackDeleteByIdCommandHandler(ICustomerFeedbackService customerFeedbackService) 
    : ICommandHandler<CustomerFeedbackDeleteByIdCommand,bool> 
{
    public async Task<bool> Handle(CustomerFeedbackDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        await customerFeedbackService.DeleteByIdAsync(request.ProductId, new CommandOptions(), cancellationToken);
        return true;
    }
}