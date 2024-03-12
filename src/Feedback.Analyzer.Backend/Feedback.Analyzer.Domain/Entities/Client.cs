using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents a client entity in the system.
/// </summary>
public class Client : AuditableEntity
{
    /// <summary>
    /// Gets or sets the first name of the client.
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the last name of the client.
    /// </summary>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the email address of the client.
    /// </summary>
    public string EmailAddress { get; set; } = default!;

    /// <summary>
    /// Gets or sets the password of the client's email address.
    /// </summary> 
    public string PasswordHash { get; set; } = default!;

    /// <summary>
    /// Gets or sets a collection of Organizations.
    /// </summary>
    public IEnumerable<Organization>? Organizations { get; set; } = default!; 
}