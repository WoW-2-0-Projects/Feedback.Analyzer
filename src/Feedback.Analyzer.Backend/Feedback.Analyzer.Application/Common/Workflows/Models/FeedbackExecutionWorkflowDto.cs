using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Application.Common.Workflows.Models;

public class FeedbackExecutionWorkflowDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public Guid ProductId { get; set; }

    public WorkflowType Type { get; set; }
}