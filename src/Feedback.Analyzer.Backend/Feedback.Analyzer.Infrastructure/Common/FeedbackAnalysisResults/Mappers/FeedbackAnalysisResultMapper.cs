using AutoMapper;
using Feedback.Analyzer.Application.Common.FeedbackAnalysisResults.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Common.FeedbackAnalysisResults.Mappers;

public class FeedbackAnalysisResultMapper : Profile
{
    public FeedbackAnalysisResultMapper()
    {
        CreateMap<FeedbackAnalysisResultDto, FeedbackAnalysisResult>().ReverseMap();
    }
}