using Feedback.Analyzer.Domain.Common.Entities;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents template of notification
/// </summary>
public abstract class NotificationTemplate : AuditableEntity
{
    /// <summary>
    /// Gets or sets type of the notification
    /// </summary>
    public NotificationType Type { get; set; }

    /// <summary>
    /// Gets or sets type of the template
    /// </summary>
    public NotificationTemplateType TemplateType { get; set; }

    /// <summary>
    /// Gets or sets content of the template
    /// </summary>
    public string Content { get; set; } = default!;

    /// <summary>
    /// Gets or sets the collection of the notification history
    /// </summary>
    public IList<NotificationHistory> Histories { get; set; } = new List<NotificationHistory>();
}
