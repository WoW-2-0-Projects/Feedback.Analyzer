using AutoMapper;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Models;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflowResults.Mappers;

public class FeedbackAnalysisWorkflowResultMapper : Profile
{
    public FeedbackAnalysisWorkflowResultMapper()
    {
        CreateMap<FeedbackAnalysisWorkflowResult, FeedbackAnalysisWorkflowResultDto>()
            .ForMember(
                dest => dest.ProcessedAnalysisCount,
                opt => opt.MapFrom(src => (ulong)src.FeedbackAnalysisResults.LongCount(result => result.AnalysisResult.Status == WorkflowStatus.Completed))
            )
            .ForMember(
                dest => dest.FailedAnalysisCount,
                opt => opt.MapFrom(src => (ulong)src.FeedbackAnalysisResults.LongCount(result => result.AnalysisResult.Status == WorkflowStatus.Failed))
            );
    }
}