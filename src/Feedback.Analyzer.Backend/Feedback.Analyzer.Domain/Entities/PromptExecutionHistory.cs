using Feedback.Analyzer.Domain.Common.Entities;
using Feedback.Analyzer.Domain.Common.Exeptions;

namespace Feedback.Analyzer.Domain.Entities;

public class PromptExecutionHistory : Entity
{ 
    public Guid PromptId { get; set; }

    public string? Result { get; set; }

    public string? Exception { get; set; } 
    
    public TimeSpan ExecutionTime { get; set; } 
    
    public TimeSpan ExecutionDuration { get; set; }
    
}