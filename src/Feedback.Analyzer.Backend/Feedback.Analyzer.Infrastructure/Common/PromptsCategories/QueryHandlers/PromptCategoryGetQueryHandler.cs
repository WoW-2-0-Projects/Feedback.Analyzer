using AutoMapper;
using AutoMapper.QueryableExtensions;
using Feedback.Analyzer.Application.Common.PromptCategory.Models;
using Feedback.Analyzer.Application.Common.PromptCategory.Quearies;
using Feedback.Analyzer.Application.Common.PromptCategory.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Common.PromptsCategories.QueryHandlers;

public class PromptCategoryGetQueryHandler(IMapper mapper, IPromptCategoryService promptCategoryService)
    : IQueryHandler<PromptCategoryGetQuery, ICollection<AnalysisPromptCategoryDto>>
{
    public async Task<ICollection<AnalysisPromptCategoryDto>> Handle(PromptCategoryGetQuery request, CancellationToken cancellationToken)
    {
        var matchedPromptCategory = await promptCategoryService.Get(
            request.Filter,
            new QueryOptions
            {
                AsNoTracking = true
            }
        )
        .Include(promptCategory => promptCategory.Prompts)
        //.ProjectTo<AnalysisPromptCategoryDto>(mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken);

        var result = mapper.Map<ICollection<AnalysisPromptCategoryDto>>(matchedPromptCategory);
        
        return result;
    }
}