using AutoMapper;
using Feedback.Analyzer.Application.Common.Prompts.Commands;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Infrastructure.Common.Prompts.CommandHandlers;

/// <summary>
/// Handles the execution of the <see cref="PromptDeleteByIdCommand"/>,
/// responsible for deleting a prompt.
/// </summary>
public class PromptDeleteByIdCommandHandler(IMapper mapper, IPromptService promptService) : ICommandHandler<PromptDeleteByIdCommand, bool>
{
    public async Task<bool> Handle(PromptDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        await promptService.DeleteByIdAsync(request.PromptId, cancellationToken: cancellationToken);
        return true;
    }
}