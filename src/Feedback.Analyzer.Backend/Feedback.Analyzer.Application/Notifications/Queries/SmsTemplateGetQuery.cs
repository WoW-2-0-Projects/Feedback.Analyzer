using Feedback.Analyzer.Application.Notifications.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Notifications.Queries;

/// <summary>
/// Represents a query to retrieve a collection of sms templates.
/// </summary>
public class SmsTemplateGetQuery : IQuery<ICollection<SmsTemplateDto>>
{
    /// <summary>
    /// Gets or sets the filter to apply when retrieving sms templates.
    /// </summary>
    public SmsTemplateFilter SmsTemplateFilter { get; set; }
}
