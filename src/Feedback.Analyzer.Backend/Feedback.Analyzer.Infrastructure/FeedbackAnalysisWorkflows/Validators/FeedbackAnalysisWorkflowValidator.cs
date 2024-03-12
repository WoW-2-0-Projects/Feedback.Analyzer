using Feedback.Analyzer.Application.Common.AnalysisWorkflows.Services;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using FluentValidation;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Validators;

public class FeedbackAnalysisWorkflowValidator
    : AbstractValidator<FeedbackAnalysisWorkflow>
{
    public FeedbackAnalysisWorkflowValidator(IAnalysisWorkflowService analysisWorkflowService)
    {
        RuleSet(EntityEvent.OnCreate.ToString(),
             () =>
            {
                RuleFor(feedbackAnalysisWorkflow => feedbackAnalysisWorkflow.Id)
                    .NotEmpty()
                    .CustomAsync(async (id, validationContext, cancellationToken) =>
                    {
                        var exists = (await analysisWorkflowService.CheckByIdAsync(id, cancellationToken)) != null;

                        if (!exists)
                            validationContext.AddFailure(
                                "Analysis workflow doesn't exist for this feedback analysis workflow");
                    });
            });
    }
}