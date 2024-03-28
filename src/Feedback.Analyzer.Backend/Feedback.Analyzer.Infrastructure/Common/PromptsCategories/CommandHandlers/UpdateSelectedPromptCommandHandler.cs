using Feedback.Analyzer.Application.Common.PromptCategory.Commands;
using Feedback.Analyzer.Application.Common.PromptCategory.Services;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Persistence.Extensions;

namespace Feedback.Analyzer.Infrastructure.Common.PromptsCategories.CommandHandlers;

public class UpdateSelectedPromptCommandHandler(IPromptCategoryService promptCategoryService, IPromptService promptService)
    : ICommandHandler<UpdateSelectedPromptCommand, bool>
{
    public async Task<bool> Handle(UpdateSelectedPromptCommand request, CancellationToken cancellationToken)
    {
        /*// Validate prompt category exists
        var promptCategory = await promptCategoryService
                                 .Get(promptCategory => promptCategory.Id == request.CategoryId)
                                 .Select(promptCategory => new { promptCategory.Id })
                                 .FirstOrDefaultAsync(cancellationToken: cancellationToken) ??
                             throw new InvalidOperationException(
                                 $"Prompt category with id {request.CategoryId} not found");

        // Validate prompt exists
        var prompt = await promptService
                         .Get(prompt => prompt.Id == request.PromptId)
                         .Select(prompt => new { prompt.Id })
                         .FirstOrDefaultAsync(cancellationToken: cancellationToken) ??
                     throw new InvalidOperationException($"Prompt with id {request.PromptId} not found");*/

       return await promptCategoryService.UpdateSelectedPromptIdAsync(request.CategoryId, request.PromptId, cancellationToken);
    }
}