using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Validators;

public class FeedbackAnalysisWorkflowValidator: AbstractValidator<FeedbackAnalysisWorkflow>
{
    public FeedbackAnalysisWorkflowValidator(IAnalysisWorkflowRepository analysisWorkflowRepository)
    {
        RuleSet(EntityEvent.OnCreate.ToString(),
             () =>
            {
                RuleFor(feedbackAnalysisWorkflow => feedbackAnalysisWorkflow.Id)
                    .NotEmpty()
                    .CustomAsync(async (id, validationContext, cancellationToken) =>
                    {
                        if (!await analysisWorkflowRepository.CheckByIdAsync(id, cancellationToken))
                            validationContext.AddFailure(
                                "Analysis workflow doesn't exist for this feedback analysis workflow");
                    });
            });
    }
}