using Feedback.Analyzer.Application.Common.Settings;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Feedback.Analyzer.Infrastructure.Organizations.Validators;

/// <summary>
/// Validator class for validating user data using FluentValidation.
/// </summary>
public class OrganizationValidator : AbstractValidator<Organization>
{
    /// <summary>
    /// The validation settings organization for user data validation.
    /// </summary>
    /// <param name="validationSettings"></param>
    public OrganizationValidator(IOptions<ValidationSettings> validationSettings)
    {
        // Rule set for creating a new organization
        RuleSet(
            EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(organization => organization.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(128);

                RuleFor(organization => organization.Description)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(512)
                    .WithMessage("Description is not valid");
            }
        );
        
        RuleSet(
            EntityEvent.OnUpdate.ToString(),
            () =>
            {
                RuleFor(organization => organization.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(128);

                RuleFor(organization => organization.Description)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(512)
                    .WithMessage("Description is not valid");
            }
        );
    }
}