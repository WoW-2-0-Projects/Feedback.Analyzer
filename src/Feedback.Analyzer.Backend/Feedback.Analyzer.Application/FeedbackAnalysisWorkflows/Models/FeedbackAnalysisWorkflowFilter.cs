using Feedback.Analyzer.Domain.Common.Query;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;

public class FeedbackAnalysisWorkflowFilter : FilterPagination
{
    /// <summary>
    /// Override base GetHashCode method
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        
            hashCode.Add(PageSize);
            hashCode.Add(PageToken);

            return hashCode.ToHashCode();
    }

    /// <summary>
    /// Override base Equals method
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj) =>
        obj is FeedbackAnalysisWorkflowFilter feedbackAnalysisWorkflowFilter
        && feedbackAnalysisWorkflowFilter.GetHashCode() == GetHashCode();
}