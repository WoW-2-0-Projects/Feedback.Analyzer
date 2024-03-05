using AutoMapper;
using Feedback.Analyzer.Application.Common.Prompts.Commands;
using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Application.Organizations.Commands;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Prompts;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Common.Prompts.CommandHandlers;

/// <summary>
/// Handles the execution of the <see cref="OrganizationCreateCommand"/>.
/// Responsible for coordinating the creation of a new organization.
/// </summary>
public class PromptCreateCommandHandler(IMapper mapper, IPromptService promptService) : ICommandHandler<PromptCreateCommand, AnalysisPromptDto>
{
    public async Task<AnalysisPromptDto> Handle(PromptCreateCommand request, CancellationToken cancellationToken)
    {
        // Conversion to domain entity cancellationToken
        var prompt = mapper.Map<AnalysisPrompt>(request.Prompt);

        // Call service
        var createdPrompt = await promptService.CreateAsync(prompt, cancellationToken: cancellationToken);

        // Conversion to DTO
        return mapper.Map<AnalysisPromptDto>(createdPrompt);
    }
}