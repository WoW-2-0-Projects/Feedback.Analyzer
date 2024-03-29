using AutoMapper;
using Feedback.Analyzer.Application.Common.PromptsHistory.Commands;
using Feedback.Analyzer.Application.Common.PromptsHistory.Models;
using Feedback.Analyzer.Application.Common.PromptsHistory.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Common.PromptsHistory.CommandHandlers;

/// <summary>
/// Command handler for creating prompt execution history records.
/// </summary>
public class PromptExecutionHistoryCreateCommandHandler(
    IMapper Mapper,
    IPromptExecutionHistoryService PromptExecutionHistoryService
    ) : ICommandHandler<PromptExecutionHistoryCreateCommand, PromptExecutionHistoryDto>
{
    public async Task<PromptExecutionHistoryDto> Handle(PromptExecutionHistoryCreateCommand request, CancellationToken cancellationToken)
    {
        var promptsExecutionHistory = Mapper.Map<PromptExecutionHistory>(request.PromptExecutionHistoryDto);
        var result = await PromptExecutionHistoryService.CreateAsync(promptsExecutionHistory, new CommandOptions(){SkipSaveChanges = false}, cancellationToken);

        return Mapper.Map<PromptExecutionHistoryDto>(result);
    }
}