using AutoMapper;
using AutoMapper.QueryableExtensions;
using Feedback.Analyzer.Application.Common.PromptCategories.Models;
using Feedback.Analyzer.Application.Common.PromptCategories.Queries;
using Feedback.Analyzer.Application.Common.PromptCategories.Services;
using Feedback.Analyzer.Application.Common.Prompts.Queries;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Common.PromptCategories.QueryHandlers;

/// <summary>
/// Handles the execution of the <see cref="PromptGetQuery"/>.
/// Responsible for retrieving a specific prompt based on filter
/// </summary>
public class PromptCategoryQueryHandler(IPromptCategoryService promptCategoryService, IMapper mapper)
    : IQueryHandler<PromptCategoryGetQuery, ICollection<AnalysisPromptCategoryDto>>
{
    public async Task<ICollection<AnalysisPromptCategoryDto>> Handle(PromptCategoryGetQuery request, CancellationToken cancellationToken)
    {
        var matchedPromptCategories = await promptCategoryService
            .Get(
                request.Filter,
                new QueryOptions
                {
                    AsNoTracking = true
                }
            )
            .Include(promptCategory => promptCategory.Prompts)
            .ProjectTo<AnalysisPromptCategoryDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return matchedPromptCategories;
    }
}