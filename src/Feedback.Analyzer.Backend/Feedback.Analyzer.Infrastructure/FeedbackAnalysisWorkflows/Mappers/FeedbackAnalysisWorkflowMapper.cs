using AutoMapper;
using Feedback.Analyzer.Application.CustomerFeedbacks.Models;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Mappers;

public class FeedbackAnalysisWorkflowMapper : Profile
{
    public FeedbackAnalysisWorkflowMapper()
    {
        CreateMap<FeedbackAnalysisWorkflow, FeedbackExecutionWorkflowDto>().ReverseMap();

        CreateMap<FeedbackAnalysisWorkflow, FeedbackAnalysisWorkflowContext>()
            .ForMember(dest => dest.WorkflowId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.EntryExecutionOption, opt => opt.MapFrom(src => new WorkflowExecutionOptions
            {
                Id = src.EntryExecutionOptionId
            }))
            .ForMember(dest => dest.Organization, opt => opt.MapFrom(src => src.Product.Organization))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
            .ForMember(dest => dest.FeedbacksId, opt => opt.MapFrom(src => src.Product.CustomerFeedbacks.Select(feedback => feedback.Id)));

        CreateMap<FeedbackAnalysisWorkflowContext, SingleFeedbackAnalysisWorkflowContext>();
    }
}