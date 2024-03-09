using Feedback.Analyzer.Domain.Common.Entities;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Domain.Entities;

public class AnalysisWorkflow : Entity
{
    public string Name { get; set; } = default!;
    
    public WorkflowType Type { get; set; }
    
    public WorkflowStatus Status { get; set; }
    
    public Guid EntryExecutionOptionId { get; set; }
    
    public WorkflowExecutionOption EntryExecutionOption { get; set; } = default!;
}