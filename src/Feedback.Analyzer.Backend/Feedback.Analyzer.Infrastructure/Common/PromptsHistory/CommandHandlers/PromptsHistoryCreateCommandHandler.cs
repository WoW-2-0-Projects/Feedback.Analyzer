using AutoMapper;
using Feedback.Analyzer.Application.Common.Prompts.Commands;
using Feedback.Analyzer.Application.Common.PromptsHistory.Commands;
using Feedback.Analyzer.Application.Common.PromptsHistory.Models;
using Feedback.Analyzer.Application.Common.PromptsHistory.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Infrastructure.Common.PromptsHistory.Validators;
using FluentValidation;

namespace Feedback.Analyzer.Infrastructure.Common.PromptsHistory.CommandHandlers;

public class PromptsHistoryCreateCommandHandler(
    IMapper mapper,
    IPromptsExecutionHistoryService promptsExecutionHistoryService
    ) : ICommandHandler<PromptsHistoryCreateCommand, PromptsExecutionHistoryDto>
{
    public async Task<PromptsExecutionHistoryDto> Handle(PromptsHistoryCreateCommand request, CancellationToken cancellationToken)
    {
        var promptsExecutionHistory = mapper.Map<PromptExecutionHistory>(request.PromptsExecutionHistoryDto);
        var result = await promptsExecutionHistoryService.CreateAsync(promptsExecutionHistory, new CommandOptions(){SkipSaveChanges = false}, cancellationToken);

        return mapper.Map<PromptsExecutionHistoryDto>(result);
    }
}