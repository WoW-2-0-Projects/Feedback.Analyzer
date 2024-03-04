using Feedback.Analyzer.Application.Common.PromptsHistory.Models;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.PromptsHistory.Commands;

public class PromptsHistoryCreateCommand : ICommand<PromptsExecutionHistoryDto>
{
    public PromptsExecutionHistoryDto PromptsExecutionHistoryDto { get; set; } = default!;
}