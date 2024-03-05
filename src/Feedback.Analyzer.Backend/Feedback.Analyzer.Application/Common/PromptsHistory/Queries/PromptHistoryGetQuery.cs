using Feedback.Analyzer.Application.Common.PromptsHistory.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Common.PromptsHistory.Queries;

public class PromptHistoryGetQuery : IQuery<IList<PromptsExecutionHistoryDto>>
{
    public PromptsHistoryFilter Filter { get; set; } = default!;
}