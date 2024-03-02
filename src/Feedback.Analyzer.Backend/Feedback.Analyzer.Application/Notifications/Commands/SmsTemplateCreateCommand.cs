using Feedback.Analyzer.Application.Notifications.Models;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.Notifications.Commands;

/// <summary>
/// Represents a command to create a new sms template.
/// </summary>
public class SmsTemplateCreateCommand : ICommand<SmsTemplateDto>
{
    /// <summary>
    /// The data required to create a new sms template.
    /// </summary>
    public SmsHistoryDto SmsTemplateDto { get; set; }
}
