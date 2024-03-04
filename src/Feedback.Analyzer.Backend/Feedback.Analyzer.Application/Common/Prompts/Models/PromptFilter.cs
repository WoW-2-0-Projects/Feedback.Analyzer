using Feedback.Analyzer.Domain.Common.Query;

namespace Feedback.Analyzer.Application.Common.Prompts.Models;

/// <summary>
/// Represents criteria for filtering and paginating prompt data.
/// </summary>
public class PromptFilter : FilterPagination
{
    /// <summary>
    /// Initializes a new instance of the Filter class with default pagination values for retrieving all prompts from the first page.
    /// </summary>
    public PromptFilter()
    {
        PageSize = int.MaxValue;
        PageToken = 1;
    }

    /// <summary>
    /// Provides a hash code for this instance based on PageSize and PageToken for efficient comparisons and usage in collections.
    /// </summary>
    /// <returns>An integer representing the hash code.</returns>
    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        hashCode.Add(PageSize);
        hashCode.Add(PageToken);

        return hashCode.ToHashCode();
    }

    /// <summary>
    /// Determines whether the current object is equal to another Filter object.
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns>True if the objects are equal, false otherwise.</returns>
    public override bool Equals(object? obj)
    {
        return obj is PromptFilter promptFilter && promptFilter.GetHashCode() == GetHashCode();
    }
}