using Feedback.Analyzer.Application.Organizations.Models;
using Feedback.Analyzer.Domain.Common.Query;

namespace Feedback.Analyzer.Application.AnalysisWorkflows.Models;

/// <summary>
/// Represents a filter for querying analysis workflows with pagination support.
/// </summary>
public class AnalysisWorkflowFilter : FilterPagination
{
    /// <summary>
    /// Computes the hash code for the current filter instance.
    /// </summary>
    /// <returns>The hash code value for the filter.</returns>
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        
        // Add the PageSize and PageToken properties to the hash code computation
        hashCode.Add(PageSize);
        hashCode.Add(PageToken);

        // Return the computed hash code
        return hashCode.ToHashCode();
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current filter instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current filter.</param>
    /// <returns>True if the specified object is equal to the current filter; otherwise, false.</returns>
    public override bool Equals(object? obj) =>
        obj is OrganizationFilter organizationFilter
        && organizationFilter.GetHashCode() == GetHashCode();
}