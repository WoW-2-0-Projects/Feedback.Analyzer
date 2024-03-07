using Feedback.Analyzer.Application.Common.PromptCategories.Services;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Application.Common.Workflows.Events;
using Feedback.Analyzer.Application.Common.Workflows.Services;
using Feedback.Analyzer.Domain.Common.Events;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Feedback.Analyzer.Infrastructure.Common.Workflows.EventHandlers;

public class ExecuteWorkflowSinglePromptEventHandler(
    IFeedbackExecutionWorkflowService workflowService,
    IPromptCategoryService promptCategoryService,
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
            { "productDescription", workflow.Product.Description },
            { "customerFeedback", workflow.Product.CustomerFeedbacks.First().Comment }
        };

        var histories = await promptExecutionProcessingService.ExecuteAsync(notification.PromptId, arguments, cancellationToken: cancellationToken);

        var history = histories.First();
        var category = await promptCategoryService
            .Get(category => category.Id == history.Prompt.CategoryId)
            .FirstAsync(cancellationToken: cancellationToken);

        
        if (category.Category == FeedbackAnalysisPromptCategory.LanguageRecognition)
        {
            var test = JsonSerializer.Deserialize<string[]>(history.Result!);
        }

        if (category.Category == FeedbackAnalysisPromptCategory.RelevanceAnalysis)     
        {
            var test = JsonSerializer.Deserialize<bool>(history.Result!);
        }

        if (category.Category == FeedbackAnalysisPromptCategory.RelevantContentExtraction)
        {
            var test = JsonSerializer.Deserialize<string>(history.Result!);
        }

        if (category.Category == FeedbackAnalysisPromptCategory.EntityIdentification)
        {
            var test = JsonSerializer.Deserialize<string>(history.Result!);
        }

        if (category.Category == FeedbackAnalysisPromptCategory.OpinionMining)
        {
            var test = JsonSerializer.Deserialize<OpinionType>(history.Result!);
            //var test = JsonSerializer.Deserialize<string[]>(history.Result!);
        }

        if (category.Category == FeedbackAnalysisPromptCategory.QuestionPointsExtraction)
        {
            var test = JsonSerializer.Deserialize<string[]>(history.Result!);
        }

        if (category.Category == FeedbackAnalysisPromptCategory.OpinionPointsExtraction)
        {
            var test = JsonSerializer.Deserialize<string[]>(history.Result!);
        }

        if (category.Category == FeedbackAnalysisPromptCategory.ActionableOpinionsAnalysis)
        {
            var test = JsonSerializer.Deserialize<string[]>(history.Result!);
        }

        if (category.Category == FeedbackAnalysisPromptCategory.ActionableQuestionsAnalysis)
        {
            var test = JsonSerializer.Deserialize<string[]>(history.Result!);
        }

        if (category.Category == FeedbackAnalysisPromptCategory.OpinionsCategoryAnalysis)
        {
            var test = JsonSerializer.Deserialize<string[]>(history.Result!);
        }

        var result = "";
    }
}