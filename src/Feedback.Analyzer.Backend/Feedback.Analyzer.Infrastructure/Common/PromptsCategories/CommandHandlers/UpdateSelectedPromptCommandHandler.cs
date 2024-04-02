using Feedback.Analyzer.Application.Common.PromptCategory.Commands;
using Feedback.Analyzer.Application.Common.PromptCategory.Services;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Persistence.Extensions;

namespace Feedback.Analyzer.Infrastructure.Common.PromptsCategories.CommandHandlers;

/// <summary>
/// Handles the command to update the selected prompt for a prompt category.
/// </summary>
public class UpdateSelectedPromptCommandHandler(IPromptCategoryService promptCategoryService, IPromptService promptService)
    : ICommandHandler<UpdateSelectedPromptCommand, bool>
{
    public async Task<bool> Handle(UpdateSelectedPromptCommand request, CancellationToken cancellationToken)
    {
       return await promptCategoryService.UpdateSelectedPromptIdAsync(request.CategoryId, request.PromptId, cancellationToken);
    }
}