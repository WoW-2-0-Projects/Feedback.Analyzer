using AutoMapper;
using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Application.Common.Prompts.Queries;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Common.PromptsCategories.QueryHandlers;

/// <summary>
/// Query handler for getting a collection of <see cref="PromptResultDto"/> by <see cref="Prompt.CategoryId"/>
/// </summary>
public class PromptResultGetByCategoryIdQueryHandler(IMapper mapper, IPromptService promptService) 
    : IQueryHandler<PromptResultGetByCategoryIdQuery, ICollection<PromptResultDto>>
{
    public async Task<ICollection<PromptResultDto>> Handle(PromptResultGetByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        var matchedPrompts = await promptService.Get
            (
                prompt => prompt.CategoryId == request.CategoryId,
                queryOptions: new QueryOptions()
                {
                    TrackingMode = QueryTrackingMode.AsNoTracking
                }
                )
            .OrderByDescending(prompt => prompt.Version)
            .ThenByDescending(prompt => prompt.Revision)
            .Include(prompt => prompt.ExecutionHistories)
            .ToListAsync(cancellationToken: cancellationToken);
        
        return mapper.Map<ICollection<PromptResultDto>>(matchedPrompts);
    }
}