using Feedback.Analyzer.Application.Common.PromptCategories.Services;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Application.Common.Workflows.Events;
using Feedback.Analyzer.Application.Common.Workflows.Services;
using Feedback.Analyzer.Domain.Common.Events;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Constants;
using Feedback.Analyzer.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Feedback.Analyzer.Infrastructure.Common.Workflows.EventHandlers;

public class ExecuteWorkflowSinglePromptEventHandler(
    IPromptCategoryService promptCategoryService,
    IFeedbackAnalysisWorkflowService workflowService,
    IPromptExecutionProcessingService promptExecutionProcessingService
) : IEventHandler<ExecuteWorkflowSinglePromptEvent>
{
    public async Task Handle(ExecuteWorkflowSinglePromptEvent notification, CancellationToken cancellationToken)
    {
        var workflow = await workflowService.Get(
                               workflow => workflow.Id == notification.WorkflowId,
                               new QueryOptions
                               {
                                   AsNoTracking = true
                               }
                           )
                           .Include(workflow => workflow.Product)
                           .ThenInclude(product => product.CustomerFeedbacks)
                           .AsSplitQuery()
                           .FirstOrDefaultAsync(cancellationToken: cancellationToken) ??
                       throw new InvalidOperationException($"Could not execute prompt, workflow with id {notification.WorkflowId} not found.");

        var arguments = new Dictionary<string, string>
        {
            { PromptConstants.ProductDescription, workflow.Product.Description },
            { PromptConstants.CustomerFeedback, workflow.Product.CustomerFeedbacks.First().Comment }
        };

        var histories = await promptExecutionProcessingService.ExecuteAsync(notification.PromptId, arguments, cancellationToken: cancellationToken);

        var history = histories.First();
        var category = await promptCategoryService
            .Get(category => category.Id == history.Prompt.CategoryId)
            .FirstAsync(cancellationToken: cancellationToken);


        if (category.Category == FeedbackAnalysisPromptCategory.LanguageRecognition)
        {
            var test = JsonConvert.DeserializeObject<string[]>(history.Result!);
        }

        if (category.Category == FeedbackAnalysisPromptCategory.RelevanceAnalysis)
        {
            var test = JsonConvert.DeserializeObject<bool>(history.Result!);
        }

        if (category.Category == FeedbackAnalysisPromptCategory.RelevantContentExtraction)
        {
            var test = JsonConvert.DeserializeObject<string>(history.Result!);
        }

        if (category.Category == FeedbackAnalysisPromptCategory.EntityIdentification)
        {
            var test = JsonConvert.DeserializeObject<string>(history.Result!);
        }

        if (category.Category == FeedbackAnalysisPromptCategory.OpinionMining)
        {
            var test = JsonConvert.DeserializeObject<OpinionType>(history.Result!);
            //var test = JsonConvert.DeserializeObject<string[]>(history.Result!);
        }

        if (category.Category == FeedbackAnalysisPromptCategory.QuestionPointsExtraction)
        {
            var test = JsonConvert.DeserializeObject<string[]>(history.Result!);
        }

        if (category.Category == FeedbackAnalysisPromptCategory.OpinionPointsExtraction)
        {
            var test = JsonConvert.DeserializeObject<string[]>(history.Result!);
        }

        if (category.Category == FeedbackAnalysisPromptCategory.ActionableOpinionsAnalysis)
        {
            var test = JsonConvert.DeserializeObject<string[]>(history.Result!);
        }

        if (category.Category == FeedbackAnalysisPromptCategory.ActionableQuestionsAnalysis)
        {
            var test = JsonConvert.DeserializeObject<string[]>(history.Result!);
        }

        if (category.Category == FeedbackAnalysisPromptCategory.OpinionsCategoryAnalysis)
        {
            var test = JsonConvert.DeserializeObject<string[]>(history.Result!);
        }

    }
}