using Feedback.Analyzer.Domain.Enums;
using System.Xml;

namespace Feedback.Analyzer.Application.Notifications.Models;

/// <summary>
/// Represents a data transfer object (DTO) for email template
/// This class will hold the properties needed to transfer organization-related data.
/// </summary>
public class EmailTemplateDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the email template
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets type of the template
    /// </summary>
    public NotificationTemplateType TemplateType { get; set; }

    /// <summary>
    /// Gets or sets content of the template
    /// </summary>
    public string Content { get; set; } = default!;

    /// <summary>
    /// Represents subject of the email template
    /// </summary>
    public string Subject { get; set; }
}
