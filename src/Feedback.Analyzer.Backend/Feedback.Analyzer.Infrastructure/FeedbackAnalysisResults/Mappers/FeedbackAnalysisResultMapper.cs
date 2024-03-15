using AutoMapper;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisResults.Mappers;

public class FeedbackAnalysisResultMapper : Profile
{
    public FeedbackAnalysisResultMapper()
    {
        CreateMap<FeedbackAnalysisResult, FeedbackAnalysisResultDto>().ReverseMap();
    }
}