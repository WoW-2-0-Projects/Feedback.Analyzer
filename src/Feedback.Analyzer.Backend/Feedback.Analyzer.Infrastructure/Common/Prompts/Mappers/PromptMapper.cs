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
            .ForMember(dest => dest.PromptId, opt => opt.MapFrom(src => src.Id));
    }
}