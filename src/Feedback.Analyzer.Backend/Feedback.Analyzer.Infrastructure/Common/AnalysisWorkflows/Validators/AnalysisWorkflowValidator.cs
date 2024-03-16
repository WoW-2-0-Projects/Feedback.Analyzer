using Feedback.Analyzer.Application.Common.Settings;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Feedback.Analyzer.Infrastructure.Common.AnalysisWorkflows.Validators;

public class AnalysisWorkflowValidator : AbstractValidator<AnalysisWorkflow>
{
    public AnalysisWorkflowValidator(IOptions<ValidationSettings> validationSettings)
    {
        RuleSet(
            EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(analysisWorkflow => analysisWorkflow.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(128);

                RuleFor(analysisWorkflow => analysisWorkflow.Type != WorkflowType.Template);
            });
        RuleSet(
            EntityEvent.OnUpdate.ToString(),
            () =>
            {
                RuleFor(analysisWorkflow => analysisWorkflow.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(128);

                RuleFor(analysisWorkflow => analysisWorkflow.Type).Custom((analysisWorkflow, context) =>
                {
                    if (analysisWorkflow != WorkflowType.Template)
                        context.AddFailure("Analysis workflow type cannot be a template.");
                });

            });
    }
}