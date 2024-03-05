namespace Feedback.Analyzer.Application.Common.Prompts.Models;

public abstract class FeedbackExecutionContext
{
    public Guid FeedbackId { get; set; }

    public Guid ProductId { get; set; }

    public uint ExecutionCount { get; set; }
    
}