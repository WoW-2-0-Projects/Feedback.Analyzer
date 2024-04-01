using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflowResults.Validators;

public class FeedbackAnalysisWorkflowResultValidator : AbstractValidator<FeedbackAnalysisWorkflowResult>
{
    public FeedbackAnalysisWorkflowResultValidator(IFeedbackAnalysisResultRepository feedbackAnalysisResultRepository)
    {
        RuleSet(
            EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(workflowResult => workflowResult.FeedbacksCount).GreaterThan(0U);
                RuleFor(workflowResult => workflowResult.FinishedTime).Null();
                RuleFor(workflowResult => workflowResult.ProcessedFeedbacksCount).Equal(0U);
                RuleFor(workflowResult => workflowResult.FailedFeedbacksCount).Equal(0U);
            }
        );

        RuleSet(
            EntityEvent.OnUpdate.ToString(),
            () =>
            {
                RuleFor(workflowResult => workflowResult.FeedbacksCount).GreaterThan(0U);

                RuleFor(workflowResult => workflowResult)
                    .Must(result => result.FailedFeedbacksCount + result.ProcessedFeedbacksCount == result.FeedbacksCount)
                    .WithMessage(
                        "The sum of processed and failed feedback results count must equal to to the count calculated before creating workflow result."
                    );

                RuleFor(workflowResult => workflowResult.FeedbackAnalysisResults)
                    .CustomAsync(
                        async (_, validationContext, cancellationToken) =>
                        {
                            // Check if feedback analysis results count is equal to the count calculated before creating workflow result
                            var feedbacksCount = await feedbackAnalysisResultRepository.Get(
                                    result => result.FeedbackAnalysisWorkflowResultId == validationContext.InstanceToValidate.Id
                                )
                                .CountAsync(cancellationToken);

                            if (feedbacksCount != validationContext.InstanceToValidate.FeedbacksCount)
                                validationContext.AddFailure(
                                    $"Created feedback analysis results count must be equal the count calculated before creating workflow result. Expected: {validationContext.InstanceToValidate.FeedbacksCount}, Actual: {feedbacksCount}"
                                );
                        }
                    );

                RuleFor(workflowResult => workflowResult.FinishedTime).NotNull().GreaterThan(workflowResult => workflowResult.StartedTime);
            }
        );
    }
}