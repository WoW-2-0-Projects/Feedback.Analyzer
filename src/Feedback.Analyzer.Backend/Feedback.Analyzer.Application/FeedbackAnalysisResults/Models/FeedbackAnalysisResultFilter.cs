using Feedback.Analyzer.Domain.Common.Query;

namespace Feedback.Analyzer.Application.FeedbackAnalysisResults.Models;

/// <summary>
/// Represents a filter for querying feedback analysis results with pagination support.
/// </summary>
public class FeedbackAnalysisResultFilter : FilterPagination
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FeedbackAnalysisResultFilter"/> class.
    /// </summary>
    public FeedbackAnalysisResultFilter()
    {
        PageSize = int.MaxValue;
        PageToken = 1;
    }
    
    /// <summary>
    /// Computes the hash code for the filter.
    /// </summary>
    /// <returns>The hash code value for the filter.</returns>
    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        hashCode.Add(PageSize);
        hashCode.Add(PageToken);

        return hashCode.ToHashCode();
    }
    
    /// <summary>
    /// Determines whether the specified object is equal to the current filter.
    /// </summary>
    /// <param name="obj">The object to compare with the current filter.</param>
    /// <returns>True if the specified object is equal to the current filter; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        return obj is FeedbackAnalysisResultFilter feedback && feedback.GetHashCode() == GetHashCode();
    }
}