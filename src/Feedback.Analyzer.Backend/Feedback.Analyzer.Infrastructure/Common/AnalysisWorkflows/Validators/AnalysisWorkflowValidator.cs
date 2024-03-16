using Feedback.Analyzer.Application.Common.Settings;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Feedback.Analyzer.Infrastructure.Common.AnalysisWorkflows.Validators;

public class AnalysisWorkflowValidator : AbstractValidator<AnalysisWorkflow>
{
    public AnalysisWorkflowValidator(IOptions<ValidationSettings> validationSettings,
        IAnalysisWorkflowRepository analysisWorkflowRepository)
    {
        RuleSet(
            EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(analysisWorkflow => analysisWorkflow.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(128);

                RuleFor(analysisWorkflow => analysisWorkflow.Type)
                    .NotEqual(WorkflowType.Template);
            });

        RuleSet(
            EntityEvent.OnUpdate.ToString(),
            () =>
            {
                RuleFor(analysisWorkflow => analysisWorkflow.Id)
                    .NotEqual(Guid.Empty)
                    .CustomAsync(async (analysisWorkflowId, context, cancellationToken) =>
                    {
                        var analysisWorkflowExists = await analysisWorkflowRepository
                            .CheckByIdAsync(analysisWorkflowId, cancellationToken: cancellationToken);
                        
                        if (!analysisWorkflowExists)
                            context.AddFailure($"Analysis workflow with Id - {analysisWorkflowId} doesn't exist");
                    });
                
                RuleFor(analysisWorkflow => analysisWorkflow.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(128);
            });
    }
}