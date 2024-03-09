using AutoMapper;
using Feedback.Analyzer.Application.Common.Workflows.Models;
using Feedback.Analyzer.Application.CustomerFeedbacks.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Mappers;

public class FeedbackExecutionWorkflowMapper : Profile
{
    public FeedbackExecutionWorkflowMapper()
    {
        CreateMap<FeedbackAnalysisWorkflow, FeedbackExecutionWorkflowDto>().ReverseMap();

        CreateMap<FeedbackAnalysisWorkflow, FeedbackAnalysisWorkflowContext>()
            .ForMember(dest => dest.WorkflowId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.EntryExecutionOption, opt => opt.MapFrom(src => new WorkflowExecutionOptions
            {
                Id = src.EntryExecutionOptionId
            }))
            // .ForMember(dest => dest.EntryExecutionOptionId, opt => opt.MapFrom(src => src.EntryExecutionOptionId))
            
            .ForMember(dest => dest.Organization, opt => opt.MapFrom(src => src.Product.Organization))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
    }
}