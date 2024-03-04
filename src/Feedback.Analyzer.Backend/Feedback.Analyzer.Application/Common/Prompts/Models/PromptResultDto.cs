namespace Feedback.Analyzer.Application.Common.Prompts.Models;

public record PromptResultDto
{
    public Guid PromptId { get; set; }
    
    public int Version { get; set; }
    
    public int Revision { get; set; }
    
    public int ExecutionsCount { get; set; }
    
    public TimeSpan AverageExecutionTime { get; set; }
    
    public int AverageAccuracy { get; set; }
}