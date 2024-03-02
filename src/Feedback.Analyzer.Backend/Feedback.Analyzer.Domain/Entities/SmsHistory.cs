using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represent notification history for sms
/// </summary>
public class SmsHistory : NotificationHistory
{
    /// <summary>
    /// Represents phone number of the notification sender user
    /// </summary>
    public string SenderPhoneNumber { get; set; }

    /// <summary>
    /// Represents phone number of the notification receiver user
    /// </summary>
    public string ReceiverPhoneNumber { get; set; }

    /// <summary>
    /// Represents sms template of the sms history
    /// </summary>
    public SmsTemplate SmsTemplate { get; set; }

    /// <summary>
    /// Initializes a new instance of the SmsHistory class.
    /// Sets the type of the template to Sms.
    /// </summary>
    public SmsHistory() => Type = NotificationType.Sms;
}
