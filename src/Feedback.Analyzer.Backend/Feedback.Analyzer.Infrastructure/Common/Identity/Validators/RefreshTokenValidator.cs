using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Infrastructure.Common.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Feedback.Analyzer.Infrastructure.Common.Identity.Validators;

/// <summary>
/// Validator for <see cref="RefreshToken"/> objects.
/// </summary>
public class RefreshTokenValidator : AbstractValidator<RefreshToken>
{
    public RefreshTokenValidator(IOptions<JwtSettings> jwtSettings)
    {
        var identityTokenSettingsValue = jwtSettings.Value;

        RuleSet(
            EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(refreshToken => refreshToken.Id).NotEqual(Guid.Empty);

                RuleFor(refreshToken => refreshToken.Token).NotEmpty();

                RuleFor(refreshToken => refreshToken.UserId).NotEqual(Guid.Empty);

                RuleFor(refreshToken => refreshToken.ExpiryTime)
                    .GreaterThan(DateTimeOffset.UtcNow)
                    .Custom(
                        (refreshToken, context) =>
                        {
                            if (refreshToken >
                                DateTimeOffset.UtcNow.AddMinutes(identityTokenSettingsValue.RefreshTokenExtendedExpirationTimeInMinutes))
                                context.AddFailure(
                                    nameof(RefreshToken.ExpiryTime),
                                    $"{nameof(RefreshToken.ExpiryTime)} cannot be greater than the expiration time of the JWT token."
                                );
                        }
                    )
                    .When(refreshToken => refreshToken.EnableExtendedExpiryTime)
                    .Custom(
                        (refreshToken, context) =>
                        {
                            if (refreshToken > DateTimeOffset.UtcNow.AddMinutes(identityTokenSettingsValue.RefreshTokenExpirationTimeInMinutes))
                                context.AddFailure(
                                    nameof(RefreshToken.ExpiryTime),
                                    $"{nameof(RefreshToken.ExpiryTime)} cannot be greater than the expiration time of the JWT token."
                                );
                        }
                    )
                    .When(refreshToken => !refreshToken.EnableExtendedExpiryTime);
            }
        );
    }
}
