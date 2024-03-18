using AutoMapper;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Mappers;

public class FeedbackAnalysisWorkflowMapper : Profile
{
    public FeedbackAnalysisWorkflowMapper()
    {
        CreateMap<FeedbackAnalysisWorkflow, FeedbackAnalysisWorkflowDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.AnalysisWorkflow.Name))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.AnalysisWorkflow.Type))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id));

        CreateMap<FeedbackAnalysisWorkflowDto, FeedbackAnalysisWorkflow>()
            .ForPath(dest => dest.AnalysisWorkflow.Type, opt => opt.MapFrom(src => src.Type))
            .ForPath(dest => dest.AnalysisWorkflow.Name, opt => opt.MapFrom(src => src.Name));
    }
}