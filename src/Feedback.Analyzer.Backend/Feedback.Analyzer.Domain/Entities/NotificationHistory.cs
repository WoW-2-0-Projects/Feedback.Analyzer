using Feedback.Analyzer.Domain.Common.Entities;
using Feedback.Analyzer.Domain.Enums;
using System.Diagnostics.Contracts;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents history of the notification
/// </summary>
public abstract class NotificationHistory : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the notification template associated with this notification history 
    /// </summary>
    public Guid TemplateId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the notification sender user
    /// </summary>
    public Guid SenderUserId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the notification receiver user
    /// </summary>
    public Guid ReceiverUserId { get; set; }

    /// <summary>
    /// Get or sets type of notification
    /// </summary>
    public NotificationType Type { get; set; }

    /// <summary>
    /// Gets or sets content of the notification
    /// </summary>
    public string Content { get; set; } = default!;

    /// <summary>
    /// Gets or sets whether this notification was sent successfully or not
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Gets or sets whether there is any error message while sending notification
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets notification template of notification history
    /// </summary>
    public NotificationTemplate Template { get; set; } = default!;
}