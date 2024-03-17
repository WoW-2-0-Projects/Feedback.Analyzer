using AutoMapper;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Modal;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflowResults.Mappers;

public class FeedbackAnalysisWorkflowResultMapper : Profile
{
    public FeedbackAnalysisWorkflowResultMapper()
    {
        CreateMap<FeedbackAnalysisWorkflowResult, FeedbackAnalysisWorkflowResultDto>()
            .ForMember(dest => dest.Results, opt => opt.MapFrom(src => src.FeedbackAnalysisResults));
    }
}