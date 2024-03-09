using Feedback.Analyzer.Application.CustomerFeedbacks.Models;

namespace Feedback.Analyzer.Application.Common.Prompts.Models;

public class FeedbackAnalysisWorkflowPromptSingleExecutionContext : FeedbackAnalysisWorkflowContext
{
    public Guid PromptId { get; set; }
    
    // public IReadOnlyList<FuncResult<string>> ExecutionResults { get; set; } = new List<FuncResult<string>>();
}