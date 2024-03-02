using Feedback.Analyzer.Application.Clients.Models;
using Feedback.Analyzer.Domain.Common.Query;

namespace Feedback.Analyzer.Application.Notifications.Models;

// <summary>
/// Represents a filter for querying sms templates with pagination support.
/// </summary>
public class SmsTemplateFilter : FilterPagination
{
    public SmsTemplateFilter()
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
        obj is ClientFilter clientFilter
        && clientFilter.GetHashCode() == GetHashCode();
}
