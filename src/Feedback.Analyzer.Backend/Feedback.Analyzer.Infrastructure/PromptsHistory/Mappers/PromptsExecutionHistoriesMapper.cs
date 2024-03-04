using AutoMapper;
using Feedback.Analyzer.Application.PromptsHistory.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.PromptsHistory.Mappers;

public class PromptsExecutionHistoriesMapper : Profile
{
    public PromptsExecutionHistoriesMapper()
    {

        CreateMap<PromptExecutionHistoryDto, PromptExecutionHistory>()
            .ForPath(dest => dest.ExecutionDuration, opt => opt.MapFrom(src => TimeSpan.FromSeconds(src.ExecutionDurationInSeconds)));

        CreateMap<PromptExecutionHistory, PromptExecutionHistoryDto>()
            .ForPath(dest => dest.ExecutionDurationInSeconds, opt => opt.MapFrom(src => src.ExecutionDuration.Seconds));
    }
    
}