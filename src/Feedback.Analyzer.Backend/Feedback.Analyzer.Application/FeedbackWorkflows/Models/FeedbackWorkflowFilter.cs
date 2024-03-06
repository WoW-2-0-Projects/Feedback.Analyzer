using Feedback.Analyzer.Domain.Common.Query;

namespace Feedback.Analyzer.Application.FeedbackWorkflows.Models;

/// <summary>
/// Represents a filter for paginated retrieval of feedback workflows.
/// </summary>
public class FeedbackWorkflowFilter : FilterPagination
{
    /// <summary>
    /// Overrides the GetHashCode method to generate a hash code based on PageSize and PageToken properties.
    /// </summary>
    /// <returns>The generated hash code for the filter.</returns>
    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        hashCode.Add(PageSize);
        hashCode.Add(PageToken);

        return hashCode.ToHashCode();
    }

    /// <summary>
    /// Overrides the Equals method to compare two FeedbackWorkflowFilter objects.
    /// </summary>
    /// <param name="obj">The object to compare with the current FeedbackWorkflowFilter instance.</param>
    /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
        => obj is FeedbackWorkflowFilter feedbackWorkflowFilter 
           && feedbackWorkflowFilter.GetHashCode() == GetHashCode();
}