using AutoMapper;
using AutoMapper.QueryableExtensions;
using Feedback.Analyzer.Application.Common.PromptCategory.Models;
using Feedback.Analyzer.Application.Common.PromptCategory.Queries;
using Feedback.Analyzer.Application.Common.PromptCategory.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Common.PromptsCategories.QueryHandlers;

/// <summary>
/// Query handler for getting prompt categories
/// </summary>
public class PromptCategoryGetQueryHandler(IMapper mapper, IPromptCategoryService promptCategoryService)
    : IQueryHandler<PromptCategoryGetQuery, ICollection<AnalysisPromptCategoryDto>>
{
    public async Task<ICollection<AnalysisPromptCategoryDto>> Handle(PromptCategoryGetQuery request, CancellationToken cancellationToken)
    {
        var matchedPromptCategory = await promptCategoryService.Get(
            request.Filter,
            new QueryOptions
            {
                TrackingMode = QueryTrackingMode.AsNoTracking
            }
        )
        .Include(promptCategory => promptCategory.Prompts)
        .ToListAsync(cancellationToken);
        
        return mapper.Map<ICollection<AnalysisPromptCategoryDto>>(matchedPromptCategory);;
    }
}