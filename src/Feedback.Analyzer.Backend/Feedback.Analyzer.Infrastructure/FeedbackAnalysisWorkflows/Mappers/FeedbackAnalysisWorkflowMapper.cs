using AutoMapper;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Mappers;

public class FeedbackAnalysisWorkflowMapper : Profile
{
    public FeedbackAnalysisWorkflowMapper()
    {
        CreateMap<FeedbackAnalysisWorkflow, FeedbackAnalysisWorkflowDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.AnalysisWorkflow.Name))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src=> src.AnalysisWorkflow.Type))
            .ReverseMap();
    }
}