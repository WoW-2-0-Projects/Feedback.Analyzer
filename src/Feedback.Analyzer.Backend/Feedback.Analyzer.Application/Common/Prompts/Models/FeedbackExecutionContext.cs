using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Application.Common.Prompts.Models;

public class FeedbackExecutionContext
{
    public Guid FeedbackId { get; set; }

    public Guid ProductId { get; set; }

    public uint ExecutionCount { get; set; }
    
    public FeedbackAnalysisResult AnalysisResult { get; set; } = default!;
    
    public List<PromptExecutionHistory> Histories { get; set; }
}