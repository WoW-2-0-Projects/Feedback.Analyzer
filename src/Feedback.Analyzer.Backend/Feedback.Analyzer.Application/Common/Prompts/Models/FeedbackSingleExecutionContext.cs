namespace Feedback.Analyzer.Application.Common.Prompts.Models;

public class FeedbackSingleExecutionContext : FeedbackExecutionContext
{
    public Guid PromptId { get; set; }
    
    // public IReadOnlyList<FuncResult<string>> ExecutionResults { get; set; } = new List<FuncResult<string>>();
}