using AutoMapper;
using Feedback.Analyzer.Application.Common.PromptsHistory.Models;
using Feedback.Analyzer.Application.Common.PromptsHistory.Queries;
using Feedback.Analyzer.Application.Common.PromptsHistory.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Common.PromptsHistory.QueryHandlers;

public class PromptsHistoryGetByPromptIdQueryHandler(IPromptsExecutionHistoryService promptsExecutionHistoryService, IMapper mapper)
    : IQueryHandler<PromptsHistoryGetByPromptIdQuery, ICollection<PromptsExecutionHistoryDto>>
{
    public async Task<ICollection<PromptsExecutionHistoryDto>> Handle(PromptsHistoryGetByPromptIdQuery request, CancellationToken cancellationToken)
    {
        var result = await promptsExecutionHistoryService.Get(
                history => history.PromptId == request.PromptId,
                new QueryOptions
                {
                    TrackingMode = QueryTrackingMode.AsNoTracking
                }
            )
            .OrderByDescending(history => history.ExecutionTime)
            .ToListAsync(cancellationToken: cancellationToken);

        return mapper.Map<List<PromptsExecutionHistoryDto>>(result);
    }
}