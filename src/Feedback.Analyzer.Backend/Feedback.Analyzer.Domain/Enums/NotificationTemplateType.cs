namespace Feedback.Analyzer.Domain.Enums;

/// <summary>
/// Represents different types of Notification template depended on the action
/// </summary>
public enum NotificationTemplateType
{
    /// <summary>
    /// Represents welcoming template for notification
    /// </summary>
    WelcomeNotification = 0,

    /// <summary>
    /// Represents email address verification template for notification when you verify email address
    /// </summary>
    EmailAddressVerificationNotification = 1,

    /// <summary>
    /// Represents phone numver verification template for notification when you verify phone number
    /// </summary>
    PhoneNumberVerificationNotification = 2,

    /// <summary>
    /// Represents referral template of notification when it's referring other thing in notification
    /// </summary>
    ReferralNotification = 3
}
