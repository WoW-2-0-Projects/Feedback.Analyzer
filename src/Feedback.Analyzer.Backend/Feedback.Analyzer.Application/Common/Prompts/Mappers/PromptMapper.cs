using AutoMapper;
using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Domain.Common.Prompts;

namespace Feedback.Analyzer.Application.Common.Prompts.Mappers;

public class PromptMapper : Profile
{
    public PromptMapper()
    {
        CreateMap<AnalysisPrompt, AnalysisPromptDto>().ReverseMap();
    }
}