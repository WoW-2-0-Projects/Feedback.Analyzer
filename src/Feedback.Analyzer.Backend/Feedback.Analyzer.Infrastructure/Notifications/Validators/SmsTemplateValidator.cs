using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using FluentValidation;

namespace Feedback.Analyzer.Infrastructure.Notifications.Validators;

public class SmsTemplateValidator : AbstractValidator<SmsTemplate>
{
    public SmsTemplateValidator()
    {
        RuleFor(template => template.Content)
            .NotNull()
            .MaximumLength(256)
            .MinimumLength(10);

        RuleFor(template => template.Type).Equal(NotificationType.Email);
    }
}
