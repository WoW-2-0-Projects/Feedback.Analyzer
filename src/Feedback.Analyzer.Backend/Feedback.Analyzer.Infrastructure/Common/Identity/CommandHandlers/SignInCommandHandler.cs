using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Feedback.Analyzer.Application.Common.Identity.Commands;
using Feedback.Analyzer.Application.Common.Identity.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Common.Identity.CommandHandlers;

/// <summary>
/// Sign in command handler
/// </summary>
public class SignInCommandHandler(IAuthService authService) : ICommandHandler<SignInCommand, (AccessToken accessToken, RefreshToken refreshToken)>
{
    public async Task<(AccessToken accessToken, RefreshToken refreshToken)> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        if (request.SignInDetails is not null)
            return await authService.SignInByEmailAsync(request.SignInDetails, cancellationToken);
        
        throw new AuthenticationException("Invalid sign in request");
    }
}