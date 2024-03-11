using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using FluentValidation;

namespace Feedback.Analyzer.Infrastructure.PromptsHistory.Validators;

/// <summary>
/// Validator for the <see cref="PromptExecutionHistory"/> entity.
/// </summary>
public class PromptsExecutionHistoryValidator : AbstractValidator<PromptExecutionHistory>
{
    public PromptsExecutionHistoryValidator()
    {
        RuleSet(EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(promptHistory => promptHistory.Result)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(32768)
                    .When(executionHistory => executionHistory.Exception is null);

                RuleFor(promptHistory => promptHistory.Exception)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(32768)
                    .When(executionHistory => executionHistory.Result is null);
            });
        
    }
}