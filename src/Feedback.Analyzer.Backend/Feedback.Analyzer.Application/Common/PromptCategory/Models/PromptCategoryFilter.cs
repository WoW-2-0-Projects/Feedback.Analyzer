using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Common.PromptCategory.Models;

/// <summary>
/// Represents a filter for prompts and pagination options.
/// </summary>
public class PromptCategoryFilter : FilterPagination
{
    public PromptCategoryFilter()
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
        return obj is PromptCategoryFilter promptFilter && promptFilter.GetHashCode() == GetHashCode();
    }
}
