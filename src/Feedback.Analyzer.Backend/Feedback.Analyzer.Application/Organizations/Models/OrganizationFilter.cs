using Feedback.Analyzer.Domain.Common.Query;

namespace Feedback.Analyzer.Api.Models.DTOs;

public class OrganizationFilter : FilterPagination
{
    public OrganizationFilter() 
    { 
        PageSize = int.MaxValue;
        PageToken = 1;
    }
    
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