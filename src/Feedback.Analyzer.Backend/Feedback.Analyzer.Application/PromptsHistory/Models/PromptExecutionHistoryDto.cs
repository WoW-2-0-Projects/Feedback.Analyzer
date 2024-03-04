namespace Feedback.Analyzer.Application.PromptsHistory.Models;

public class PromptExecutionHistoryDto
{ 
    public Guid PromptsExecutionHistoryId { get; set; } 
    
    public Guid PromptId { get; set; }

    public string? Result { get; set; }

    public string? Exception { get; set; } 
    
    public DateTime ExecutionTime { get; set; } 
    
    public int ExecutionDurationInSeconds { get; set; }
}