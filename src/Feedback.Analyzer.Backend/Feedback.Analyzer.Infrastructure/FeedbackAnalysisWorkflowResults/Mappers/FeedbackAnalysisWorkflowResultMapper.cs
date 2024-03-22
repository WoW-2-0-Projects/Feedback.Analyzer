using AutoMapper;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Modal;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflowResults.Mappers;

public class FeedbackAnalysisWorkflowResultMapper : Profile
{
    public FeedbackAnalysisWorkflowResultMapper()
    {
        CreateMap<FeedbackAnalysisWorkflowResult, FeedbackAnalysisWorkflowResultDto>();
    }
}