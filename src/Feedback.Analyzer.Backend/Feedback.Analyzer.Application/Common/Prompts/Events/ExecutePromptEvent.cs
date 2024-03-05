using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Domain.Common.Events;

namespace Feedback.Analyzer.Application.Common.Prompts.Events;

public class ExecutePromptEvent : Event
{
    public FeedbackExecutionContext ExecutionContext { get; set; } = default!;
}