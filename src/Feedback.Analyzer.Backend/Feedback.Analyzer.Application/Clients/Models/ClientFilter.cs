using Feedback.Analyzer.Domain.Common.Query;

namespace Feedback.Analyzer.Application.Clients.Models;

/// <summary>
/// Represents a filter for querying clients with pagination support.
/// </summary>
public class ClientFilter : FilterPagination
{
    /// <summary>
    /// Overrides base GetHashCode method
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
    /// Overrides base Equals method
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj) => 
        obj is ClientFilter clientFilter 
        && clientFilter.GetHashCode() == GetHashCode();
}