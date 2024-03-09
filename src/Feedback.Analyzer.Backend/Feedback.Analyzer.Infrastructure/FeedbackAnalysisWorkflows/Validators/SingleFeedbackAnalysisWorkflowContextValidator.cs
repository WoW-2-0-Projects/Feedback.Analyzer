using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Enums;
using FluentValidation;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Validators;

public class SingleFeedbackAnalysisWorkflowContextValidator : AbstractValidator<SingleFeedbackAnalysisWorkflowContext>
{
    public SingleFeedbackAnalysisWorkflowContextValidator()
    {
        RuleSet(FeedbackAnalysisPromptCategory.RelevanceAnalysis.ToString(),
            () =>
            {
            });
        
        RuleSet(FeedbackAnalysisPromptCategory.RelevantContentExtraction.ToString(),
            () =>
            {
            });
        
        RuleSet(FeedbackAnalysisPromptCategory.PersonalInformationRedaction.ToString(),
            () =>
            {
            });
        
        RuleSet(FeedbackAnalysisPromptCategory.OpinionMining.ToString(),
            () =>
            {
            });
    }
}