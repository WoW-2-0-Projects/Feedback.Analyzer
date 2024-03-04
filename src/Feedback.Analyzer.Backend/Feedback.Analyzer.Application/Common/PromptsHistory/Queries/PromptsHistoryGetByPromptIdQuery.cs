using Feedback.Analyzer.Application.Common.PromptsHistory.Models;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.PromptsHistory.Queries;

public class PromptsHistoryGetByPromptIdQuery : IQuery<IList<PromptsExecutionHistoryDto>>
{
    public Guid PromptId { get; set; } = default!;
}