using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using FluentValidation;

namespace Feedback.Analyzer.Infrastructure.FeedbackWorkflows.Validators;

/// <summary>
/// Represents a validator for the FeedbackWorkflow entity.
/// </summary>
public class FeedbackWorkflowValidator : AbstractValidator<FeedbackWorkflow>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FeedbackWorkflowValidator"/> class.
    /// </summary>
    public FeedbackWorkflowValidator()
    {
        // Define validation rules for the "OnCreate" rule set.
        RuleSet(
            EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(organization => organization.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(128);
            }
        );
        
        // Define validation rules for the "OnUpdate" rule set.
        RuleSet(
            EntityEvent.OnUpdate.ToString(),
            () =>
            {
                RuleFor(organization => organization.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(128);
            }
        );
    }
}
