using Feedback.Analyzer.Application.Common.Settings;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Feedback.Analyzer.Infrastructure.Products.Validators;

/// <summary>
/// Validates the properties of a <see cref="Product"/> object.
/// </summary>
public class ProductValidator : AbstractValidator<Product>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductValidator"/> class.
    /// </summary>
    /// <param name="validationSettings">The validation settings.</param>
    public ProductValidator(IOptions<ValidationSettings> validationSettings)
    {
        var validationSettingsValue = validationSettings.Value;

        RuleSet(
            EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(product => product.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(128);
                
                RuleFor(product => product.Description)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(512);
            }
        );
        
        RuleSet(
            EntityEvent.OnUpdate.ToString(),
            () =>
            {
                RuleFor(product => product.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(128);
                
                RuleFor(product => product.Description)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(512);
            }
        );
    }
}
