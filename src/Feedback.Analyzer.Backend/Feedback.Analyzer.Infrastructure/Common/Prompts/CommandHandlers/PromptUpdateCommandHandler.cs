using AutoMapper;
using Feedback.Analyzer.Application.Common.Prompts.Commands;
using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Common.Prompts.CommandHandlers;

/// <summary>
/// Handles the execution of the <see cref="PromptUpdateCommand"/>. Responsible for creating a prompt
/// </summary>
public class PromptUpdateCommandHandler(IMapper mapper, IPromptService promptService) : ICommandHandler<PromptUpdateCommand, AnalysisPromptDto>
{
    public async Task<AnalysisPromptDto> Handle(PromptUpdateCommand request, CancellationToken cancellationToken)
    {
        // Conversion to domain entity cancellationToken
        var prompt = mapper.Map<AnalysisPrompt>(request.Prompt);

        // Call service
        var updatedPrompt = await promptService.UpdateAsync(prompt, cancellationToken: cancellationToken);

        // Conversion to DTO
        return mapper.Map<AnalysisPromptDto>(updatedPrompt);
    }
}