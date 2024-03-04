using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents notification template for sms
/// </summary>
public class SmsTemplate :NotificationTemplate
{
    /// <summary>
    /// Initializes a new instance of the SmsTemplate class.
    /// Sets the type of the template to Sms.
    /// </summary>
    public SmsTemplate() => Type = NotificationType.Sms;
}
