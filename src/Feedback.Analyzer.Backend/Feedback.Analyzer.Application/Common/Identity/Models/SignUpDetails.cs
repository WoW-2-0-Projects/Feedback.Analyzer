namespace Feedback.Analyzer.Application.Common.Identity.Models;

/// <summary>
///  Represents sign up details for authorization
/// </summary>
public class SignUpDetails
{
    /// <summary>
    /// Gets or sets first name of the sign up details
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Gets or sets last name of the sign up details
    /// </summary>
    public string LastName { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets email address of the sign up details
    /// </summary>
    public string EmailAddress { get; set; } = default!;
}