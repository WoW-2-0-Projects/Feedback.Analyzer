using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using FluentValidation;

namespace Feedback.Analyzer.Infrastructure.Common.Prompts.Validators;

public class PromptValidator : AbstractValidator<AnalysisPrompt>
{
    public PromptValidator()
    {
        RuleSet(EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(prompt => prompt.Prompt)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(32768);
            });
        
        RuleSet(EntityEvent.OnUpdate.ToString(),
            () =>
            {
                RuleFor(prompt => prompt.Prompt)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(32768);
            });
    }   
}