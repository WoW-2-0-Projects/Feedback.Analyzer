using AutoMapper;
using Feedback.Analyzer.Application.PromptsHistory.Models;
using Feedback.Analyzer.Application.PromptsHistory.Queries;
using Feedback.Analyzer.Application.PromptsHistory.Services;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Infrastructure.PromptsHistory.QueryHandlers;

/// <summary>
/// Handles the query to retrieve prompt execution history records by prompt ID.
/// </summary>
public class PromptExecutionHistoryGetByPromptIdQueryHandler(IPromptExecutionHistoryService PromptExecutionHistoryService, IMapper mapper) : IQueryHandler<PromptExecutionHistoryGetByPromptIdQuery, IList<PromptExecutionHistoryDto>>
{
    public async Task<IList<PromptExecutionHistoryDto>> Handle(PromptExecutionHistoryGetByPromptIdQuery request, CancellationToken cancellationToken)
    {
        var promptId = request.PromptId;
        var result = await PromptExecutionHistoryService.GetByPromptIdAsync(promptId, new QueryOptions() { TrackingMode = QueryTrackingMode.AsNoTracking }, cancellationToken);

        return mapper.Map<List<PromptExecutionHistoryDto>>(result);
    }
}