namespace Feedback.Analyzer.Domain.Enums;

/// <summary>
/// Represents different events of the notification
/// </summary>
public enum NotificationEvent
{
    /// <summary>
    /// Represents rendering event of the notification
    /// </summary>
    OnRendering = 0,

    /// <summary>
    /// Represents sending event of the notification
    /// </summary>
    OnSending = 1,

    /// <summary>
    /// Represents saving event of the notification
    /// </summary>
    OnSaving = 2
}
