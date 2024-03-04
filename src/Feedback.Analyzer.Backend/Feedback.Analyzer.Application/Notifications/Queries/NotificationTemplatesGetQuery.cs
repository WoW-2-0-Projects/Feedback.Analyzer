using Feedback.Analyzer.Application.Notifications.Models;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Notifications.Queries;

/// <summary>
/// Represents a query to retrieve a collection of notification templates
/// </summary>
public class NotificationTemplatesGetQuery : IQuery<ICollection<NotificationTemplate>>
{
    /// <summary>
    /// Gets or sets the filter to apply when retrieving notification templates.
    /// </summary>
    public NotificationTemplateFilter NotificationTemplateFilter { get; set; }
}
