using AutoMapper;
using Feedback.Analyzer.Application.Common.PromptCategory.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Common.PromptsCategories.Mappers;

public class PromptCategoryMappers : Profile
{
    public PromptCategoryMappers()
    {
        CreateMap<AnalysisPromptCategory, AnalysisPromptCategoryDto>()
            .ForMember(dest => dest.PromptsCount, opt => opt.MapFrom(src => src.Prompts.Count))
            .ForMember(dest => dest.SelectedPromptVersion, opt =>
                opt.MapFrom(src => src.SelectedPrompt != null ? $"{src.SelectedPrompt!.Version}.{src.SelectedPrompt!.Revision}" : null)
            ).ReverseMap();
    }
    
}