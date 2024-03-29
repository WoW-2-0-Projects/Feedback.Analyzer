using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;

/// <summary>
/// Represents a filter for querying feedback analysis workflows with pagination support.
/// </summary>
public class FeedbackAnalysisWorkflowFilter : FilterPagination
{
    /// <summary>
    /// Gets or sets client id as workflow owner
    /// </summary>
    public Guid? ClientId { get; set; }

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