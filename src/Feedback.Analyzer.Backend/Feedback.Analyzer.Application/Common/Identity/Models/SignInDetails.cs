namespace Feedback.Analyzer.Application.Common.Identity.Models;

/// <summary>
/// Represents sign in details by email
/// </summary>
public class SignInDetails
{
    /// <summary>
    /// Gets sign in email address
    /// </summary>
    public string EmailAddress { get; set; } = default!;
    
    /// <summary>
    /// Gets sign in password
    /// </summary>
    public string Password { get; set; } = default!;

    /// <summary>
    /// Gets sign extended active time
    /// </summary>
    public bool RememberMe { get; set; }
}