using AutoMapper;
using Feedback.Analyzer.Application.PromptsHistory.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.PromptsHistory.Mappers;

/// <summary>
/// Mapper profile for mapping between <see cref="PromptExecutionHistory"/> and <see cref="PromptExecutionHistoryDto"/>.
/// </summary>
public class PromptExecutionHistoriesMapper : Profile
{
    public PromptExecutionHistoriesMapper()
    {

        CreateMap<PromptExecutionHistoryDto, PromptExecutionHistory>()
            .ForPath(dest => dest.ExecutionDurationInMilliSeconds, opt => opt.MapFrom(src => TimeSpan.FromSeconds(src.ExecutionDurationInMilliseconds)));

        CreateMap<PromptExecutionHistory, PromptExecutionHistoryDto>()
            .ForPath(dest => dest.ExecutionDurationInMilliseconds, opt => opt.MapFrom(src => src.ExecutionDurationInMilliSeconds.Seconds));
    }
    
}