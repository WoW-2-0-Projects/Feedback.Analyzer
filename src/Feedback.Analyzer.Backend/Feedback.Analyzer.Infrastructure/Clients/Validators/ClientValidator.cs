using Feedback.Analyzer.Application.Common.Settings;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Feedback.Analyzer.Infrastructure.Clients.Validators;

public class ClientValidator : AbstractValidator<Client>
{
    public ClientValidator(IOptions<ValidationSettings> validationSettings)
    {
        var validationSettingsValue = validationSettings.Value;
        
        RuleSet(
            EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(client => client.FirstName)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(64)
                    .Matches(validationSettingsValue.NameRegexPattern);

                RuleFor(client => client.LastName)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(64)
                    .Matches(validationSettingsValue.NameRegexPattern);
                    

                RuleFor(client => client.EmailAddress)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(128)
                    .Matches(validationSettingsValue.EmailRegexPattern);

                RuleFor(client => client.PasswordHash).NotEmpty();
            }
        );
        
        RuleSet(
            EntityEvent.OnUpdate.ToString(),
            () =>
            {
                RuleFor(client => client.FirstName)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(64)
                    .Matches(validationSettingsValue.NameRegexPattern);

                RuleFor(client => client.LastName)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(64)
                    .Matches(validationSettingsValue.NameRegexPattern);
                    

                RuleFor(client => client.EmailAddress)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(128)
                    .Matches(validationSettingsValue.EmailRegexPattern);

                RuleFor(client => client.PasswordHash).NotEmpty();
            }
        );

        
    }
}