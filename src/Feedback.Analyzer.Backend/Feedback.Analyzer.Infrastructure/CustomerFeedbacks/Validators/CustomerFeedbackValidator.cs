using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using FluentValidation;

namespace Feedback.Analyzer.Infrastructure.CustomerFeedbacks.Validators;

public class CustomerFeedbackValidator : AbstractValidator<CustomerFeedback>
{
    public CustomerFeedbackValidator()
    {
        RuleSet(EntityEvent.OnCreate.ToString(), () =>
        {
            RuleFor(feedback => feedback.Comment).NotEmpty().MaximumLength(2048);
            RuleFor(feedback => feedback.UserName).NotEmpty().MaximumLength(64);
        });
        
        RuleSet(EntityEvent.OnUpdate.ToString(), () =>
        {
            RuleFor(feedback => feedback.Comment).NotEmpty().MaximumLength(2048);
            RuleFor(feedback => feedback.UserName).NotEmpty().MaximumLength(64);
        });
    }
    
    
    
    
}