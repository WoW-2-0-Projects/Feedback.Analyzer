using System.Threading;
using System.Threading.Tasks;
using Feedback.Analyzer.Application.Common.Identity.Commands;
using Feedback.Analyzer.Application.Common.Identity.Services;
using Feedback.Analyzer.Domain.Brokers;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Extension;

namespace Feedback.Analyzer.Infrastructure.Common.Identity.CommandHandlers;

/// <summary>
/// Represents a command handler to sign out a user.
/// </summary>
public class SignOutCommandHandler(IRequestClientContextProvider requestClientContextProvider, IAuthService authService) : ICommandHandler<SignOutCommand, bool>
{
    public async Task<bool> Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        var accessToken = requestClientContextProvider.GetAccessToken()!;
        var signOutTask = () => authService.SignOutAsync(accessToken, request.RefreshToken, cancellationToken);
        var signOutResult = await signOutTask.GetValueAsync();

        return signOutResult.IsSuccess;
    }
}

