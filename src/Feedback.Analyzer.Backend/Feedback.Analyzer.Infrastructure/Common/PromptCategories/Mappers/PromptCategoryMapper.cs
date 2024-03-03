using AutoMapper;
using Feedback.Analyzer.Application.Common.PromptCategories.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Common.PromptCategories.Mappers;

public class PromptCategoryMapper : Profile
{
    public PromptCategoryMapper()
    {
        CreateMap<AnalysisPromptCategory, AnalysisPromptCategoryDto>()
            .ForMember(dest => dest.PromptsCount, opt => opt.MapFrom(src => src.Prompts.Count))
            .ReverseMap();
    }
}