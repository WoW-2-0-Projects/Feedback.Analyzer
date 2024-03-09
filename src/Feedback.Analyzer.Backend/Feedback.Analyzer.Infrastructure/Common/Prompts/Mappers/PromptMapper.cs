using AutoMapper;
using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Common.Prompts.Mappers;

public class PromptMapper : Profile
{
    public PromptMapper()
    {
        CreateMap<AnalysisPrompt, AnalysisPromptDto>().ReverseMap();

        CreateMap<AnalysisPrompt, PromptResultDto>()
            .ForMember(dest => dest.PromptId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ExecutionsCount, opt => opt.MapFrom(src => src.ExecutionHistories.Count()))
            .ForMember(
                dest => dest.AverageExecutionTimeInSeconds,
                opt =>
                {
                    opt.PreCondition(src => src.ExecutionHistories.Any());
                    opt.MapFrom(
                        src => src.ExecutionHistories.Select(history => history.ExecutionDuration).Average(timeSpan => timeSpan.TotalMilliseconds)
                    );
                }
            );
    }
}