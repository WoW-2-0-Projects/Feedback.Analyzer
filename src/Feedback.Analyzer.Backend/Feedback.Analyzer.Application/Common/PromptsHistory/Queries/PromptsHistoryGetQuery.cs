using Feedback.Analyzer.Application.Common.PromptsHistory.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Common.PromptsHistory.Queries;

public class PromptsHistoryGetQuery : IQuery<ICollection<PromptsExecutionHistoryDto>>
{
    public PromptsHistoryFilter Filter { get; set; } = default!;
}