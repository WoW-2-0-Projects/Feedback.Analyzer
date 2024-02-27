namespace Feedback.Analyzer.Api.Models.DTOs;

public class ClientDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the listing.
    /// </summary>
    public Guid Id { get; set; }

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
    public string Email { get; set; } = default!;
}