using System.ComponentModel.DataAnnotations.Schema;
using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

public class FeedbackExecutionWorkflow : AuditableEntity
{
    public Guid ProductId { get; set; }
    
    public List<WorkflowPromptCategoryExecutionOptions> FeedbackWorkflowExecutionOptions { get; set; } = default!;
}