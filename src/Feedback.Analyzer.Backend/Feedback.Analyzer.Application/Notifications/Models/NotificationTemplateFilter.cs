using Feedback.Analyzer.Domain.Common.Query;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Application.Notifications.Models;

/// <summary>
/// Represents a filter for querying notification templates with pagination support.
/// </summary>
public class NotificationTemplateFilter : FilterPagination
{
    public NotificationTemplateFilter()
    {
        PageSize = int.MaxValue;
        PageToken = 1;
    }

    /// <summary>
    /// Gets or sets collectio of notification types for filtering templates
    /// </summary>
    public IList<NotificationType> TemplateType { get; set; }
}
