using Feedback.Analyzer.Application.Common.Workflows.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Common.Workflows.Queries;

public record WorkflowGetQuery : IQuery<ICollection<FeedbackExecutionWorkflowDto>>
{
    public FeedbackExecutionWorkflowFilter Filter { get; set; } = default!;
}