using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using FluentValidation;

namespace Feedback.Analyzer.Infrastructure.CustomerFeedbacks.Validators;

/// <summary>
/// Validator for the <see cref="CustomerFeedback"/> entity.
/// </summary>
public class CustomerFeedbackValidator : AbstractValidator<CustomerFeedback>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerFeedbackValidator"/> class.
    /// </summary>
    public CustomerFeedbackValidator()
    {
        RuleSet(EntityEvent.OnCreate.ToString(), () =>
        {
            RuleFor(feedback => feedback.UserName)
                .NotEmpty()
                .MaximumLength(64);
            
            RuleFor(feedback => feedback.Comment)
                .NotEmpty()
                .MaximumLength(2048)
                .WithMessage("Comment is out of bounds");
        });
        
        RuleSet(EntityEvent.OnUpdate.ToString(), () =>
        {
            RuleFor(feedback => feedback.UserName)
                .NotEmpty()
                .MaximumLength(64);
            
            RuleFor(feedback => feedback.Comment)
                .NotEmpty()
                .MaximumLength(2048)
                .WithMessage("Comment is out of bounds");
        });
    }
}