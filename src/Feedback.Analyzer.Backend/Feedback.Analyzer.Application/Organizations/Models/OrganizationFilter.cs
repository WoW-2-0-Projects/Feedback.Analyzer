using Feedback.Analyzer.Domain.Common.Query;

namespace Feedback.Analyzer.Application.Organizations.Models;

public class OrganizationFilter : FilterPagination
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
        obj is OrganizationFilter clientFilter 
        && clientFilter.GetHashCode() == GetHashCode();
}