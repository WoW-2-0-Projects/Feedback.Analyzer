using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents notification template for email
/// </summary>
public class EmailTemplate : NotificationTemplate
{
    /// <summary>
    /// Represents subject of the email template
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// Initializes a new instance of the EmailTemplate class.
    /// Sets the type of the template to Email.
    /// </summary>
    public EmailTemplate() => Type = NotificationType.Email;
}
