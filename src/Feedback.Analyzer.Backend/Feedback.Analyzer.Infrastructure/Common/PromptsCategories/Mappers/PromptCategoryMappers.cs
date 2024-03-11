using AutoMapper;
using Feedback.Analyzer.Application.Common.PromptCategory.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Common.PromptsCategories.Mappers;

public class PromptCategoryMappers : Profile
{
    public PromptCategoryMappers()
    {
        CreateMap<AnalysisPromptCategory, AnalysisPromptCategoryDto>()
            .ForMember(dest 
                => dest.PromptsCount, opt 
                => opt.MapFrom(anl => anl.Prompts.Count))
            .ReverseMap();
    }
    
}