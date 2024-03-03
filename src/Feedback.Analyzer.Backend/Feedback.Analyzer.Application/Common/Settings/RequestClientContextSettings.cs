namespace Feedback.Analyzer.Application.Common.Settings;

/// <summary>
/// Represents settings related to the user context for a request.
/// </summary>
public class RequestClientContextSettings
{ 
    /// <summary>
    /// Gets or sets the unique identifier of the system user associated with the request.
    /// </summary>
    public Guid SystemUserId { get; set; }
}