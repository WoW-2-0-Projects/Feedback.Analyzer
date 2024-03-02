using Feedback.Analyzer.Application.Notifications.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Notifications.Queries;

/// <summary>
/// Represents a query to retrieve a collection of email templates.
/// </summary>
public class EmailTemplateGetQuery : IQuery<ICollection<EmailTemplateDto>>
{
    /// <summary>
    /// Gets or sets the filter to apply when retrieving email templates.
    /// </summary>
    public EmailTemplateFilter EmailTemplateFilter { get; set; }
}
