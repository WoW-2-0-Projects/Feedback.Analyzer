using Feedback.Analyzer.Application.Common.Identity.Models;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.Identity.Commands;

/// <summary>
/// Represents sign in command
/// </summary>
public class SignInCommand : ICommand<(AccessToken accessToken, RefreshToken refreshToken)>
{
    /// <summary>
    /// Sign in by email method
    /// </summary>
    public SignInDetails? SignInDetails { get; set; }   
   
}