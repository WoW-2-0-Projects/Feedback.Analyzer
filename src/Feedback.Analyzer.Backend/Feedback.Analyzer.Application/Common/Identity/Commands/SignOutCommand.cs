using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.Common.Identity.Commands;

/// <summary>
/// Represents a command to sign out a user.
/// </summary>
public class SignOutCommand : ICommand<bool>
{
    /// <summary>
    /// User's refresh token
    /// </summary>
    public string RefreshToken { get; set; } = default!;
}