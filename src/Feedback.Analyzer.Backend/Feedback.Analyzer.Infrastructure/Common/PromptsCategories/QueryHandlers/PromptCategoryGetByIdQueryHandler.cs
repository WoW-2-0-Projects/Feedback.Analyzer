using AutoMapper;
using Feedback.Analyzer.Application.Common.PromptCategory.Models;
using Feedback.Analyzer.Application.Common.PromptCategory.Queries;
using Feedback.Analyzer.Application.Common.PromptCategory.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Common.PromptsCategories.QueryHandlers;

public class PromptCategoryGetByIdQueryHandler(IMapper mapper, IPromptCategoryService promptCategoryService)
    : IQueryHandler<PromptCategoryGetByIdQuery, AnalysisPromptCategoryDto?>
{
    public async Task<AnalysisPromptCategoryDto?> Handle(PromptCategoryGetByIdQuery request, CancellationToken cancellationToken)
    {
        var foundCategory = await promptCategoryService.Get(
                category => category.Id == request.CategoryId,
                new QueryOptions
                {
                    TrackingMode = QueryTrackingMode.AsNoTracking
                }
            )
            .Include(category => category.SelectedPrompt)
            .FirstOrDefaultAsync(cancellationToken);

        return mapper.Map<AnalysisPromptCategoryDto>(foundCategory);
    }
}