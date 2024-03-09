using AutoMapper;
using Feedback.Analyzer.Application.CustomerFeedbacks.Models;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Mappers;

public class FeedbackAnalysisWorkflowMapper : Profile
{
    public FeedbackAnalysisWorkflowMapper()
    {
        CreateMap<FeedbackAnalysisWorkflow, FeedbackExecutionWorkflowDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Workflow.Name))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Workflow.Type))
            .ReverseMap();

        CreateMap<FeedbackAnalysisWorkflow, FeedbackAnalysisWorkflowContext>()
            .ForMember(dest => dest.WorkflowId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.EntryExecutionOption, opt => opt.MapFrom(src => new WorkflowExecutionOption
            {
                Id = src.Workflow.EntryExecutionOptionId
            }))
            .ForMember(dest => dest.Organization, opt => opt.MapFrom(src => src.Product.Organization))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
            .ForMember(dest => dest.FeedbacksId, opt => opt.MapFrom(src => src.Product.CustomerFeedbacks.Select(feedback => feedback.Id)));

        CreateMap<FeedbackAnalysisWorkflowContext, SingleFeedbackAnalysisWorkflowContext>()
            .ForMember(dest => dest.Result, opt => opt.MapFrom(src => new FeedbackAnalysisResult()));
    }
}