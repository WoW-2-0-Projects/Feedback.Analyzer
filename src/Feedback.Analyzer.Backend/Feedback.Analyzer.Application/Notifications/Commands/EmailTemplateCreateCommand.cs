using Feedback.Analyzer.Application.Notifications.Models;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.Notifications.Commands;

/// <summary>
/// Represents a command to create a new email template.
/// </summary>
public class EmailTemplateCreateCommand : ICommand<EmailTemplateDto>
{
    /// <summary>
    /// The data required to create a new email template.
    /// </summary>
    public EmailHistoryDto EmailTemplateDto { get; set; }
}
