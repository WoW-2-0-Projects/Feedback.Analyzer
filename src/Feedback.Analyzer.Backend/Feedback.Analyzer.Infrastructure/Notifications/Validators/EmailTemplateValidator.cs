using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using FluentValidation;

namespace Feedback.Analyzer.Infrastructure.Notifications.Validators;

public class EmailTemplateValidator : AbstractValidator<EmailTemplate>
{
    public EmailTemplateValidator()
    {
        RuleFor(template => template.Content)
            .NotNull()
            .MaximumLength(256)
            .MinimumLength(10);

        RuleFor(template => template.Type).Equal(NotificationType.Email);
    }
}
