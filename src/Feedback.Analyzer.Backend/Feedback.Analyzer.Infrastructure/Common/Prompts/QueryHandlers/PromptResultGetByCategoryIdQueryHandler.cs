using AutoMapper;
using AutoMapper.QueryableExtensions;
using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Application.Common.Prompts.Queries;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Application.Common.PromptsHistory.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Common.Prompts.QueryHandlers;

public class PromptResultGetByCategoryIdQueryHandler(IMapper mapper, IPromptService promptService, IPromptsExecutionHistoryService promptsExecutionHistoryService)
    : IQueryHandler<PromptResultGetByCategoryIdQuery, ICollection<PromptResultDto>>
{
    public async Task<ICollection<PromptResultDto>> Handle(PromptResultGetByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        // TODO : inject prompt execution history service
        var matchedPrompts = await promptService.Get(
                prompt => prompt.CategoryId == request.CategoryId,
                queryOptions: new QueryOptions
                {
                    AsNoTracking = true
                }
            )
            .ProjectTo<PromptResultDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken: cancellationToken);
        
        // Aggregate history results
        // var executionHistories = await ex
        

        return matchedPrompts;
    }
}