using AutoMapper;
using Feedback.Analyzer.Application.Common.PromptsHistory.Models;
using Feedback.Analyzer.Application.Common.PromptsHistory.Queries;
using Feedback.Analyzer.Application.Common.PromptsHistory.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Common.PromptsHistory.QueryHandlers;

public class PromptsHistoryGetByPromptIdQueryHandler(IPromptsExecutionHistoryService promptsExecutionHistoryService, IMapper mapper) : IQueryHandler<PromptsHistoryGetByPromptIdQuery, IList<PromptsExecutionHistoryDto>>
{
    public async Task<IList<PromptsExecutionHistoryDto>> Handle(PromptsHistoryGetByPromptIdQuery request, CancellationToken cancellationToken)
    {
        var promptId = request.PromptId;
        var result = await promptsExecutionHistoryService.GetByPromptIdAsync(promptId, new QueryOptions() { AsNoTracking = true }, cancellationToken);

        return mapper.Map<List<PromptsExecutionHistoryDto>>(result);
    }
}