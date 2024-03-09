using AutoMapper;
using Feedback.Analyzer.Application.Common.PromptResults.Queries;
using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Common.PromptResults.Queries;

/// <summary>
/// Represents a query handler to retrieve prompt results by prompt ID.
/// </summary>
public class PromptResultGetByPromptIdQueryHandler(
    IMapper mapper,
    IPromptService promptService
) : IQueryHandler<PromptResultGetByPromptIdQuery, PromptResultDto?>
{
    public async Task<PromptResultDto?> Handle(PromptResultGetByPromptIdQuery request, CancellationToken cancellationToken)
    {
        var foundPrompt = await promptService.Get(
                prompt => prompt.Id == request.PromptId,
                queryOptions: new QueryOptions
                {
                    TrackingMode = QueryTrackingMode.AsNoTracking
                }
            )
            .Include(prompt => prompt.ExecutionHistories)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            // .ProjectTo<PromptResultDto>(mapper.ConfigurationProvider)
            // .ToListAsync(cancellationToken: cancellationToken);

        // Aggregate history results

        return mapper.Map<PromptResultDto>(foundPrompt);
    }
}