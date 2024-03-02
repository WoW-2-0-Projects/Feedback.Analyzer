using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represent notification history for email
/// </summary>
public class EmailHistory : NotificationHistory
{
    /// <summary>
    /// Represents email address of the notification sender user
    /// </summary>
    public string SenderEmailAddress { get; set; } = default!;

    /// <summary>
    /// Represents email address of the notification receiver user
    /// </summary>
    public string ReceiverEmailAddress { get; set; } = default!;

    /// <summary>
    /// Represents subject of the email history
    /// </summary>
    public string Subject { get; set; } = default!;

    /// <summary>
    /// Represents email template of the email history
    /// </summary>
    public EmailTemplate EmailTemplate { get; set; } = default!;

    /// <summary>
    /// Initializes a new instance of the email notification request class.
    /// Sets the type of the notification history to Email.
    /// </summary>
    public EmailHistory() => Type = NotificationType.Email;
}
