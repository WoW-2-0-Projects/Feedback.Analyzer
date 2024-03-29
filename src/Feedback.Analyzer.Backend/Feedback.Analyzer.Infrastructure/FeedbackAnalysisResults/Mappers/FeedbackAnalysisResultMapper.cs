using AutoMapper;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisResults.Mappers;

public class FeedbackAnalysisResultMapper : Profile
{
    public FeedbackAnalysisResultMapper()
    {
        CreateMap<FeedbackAnalysisResult, FeedbackAnalysisResultDto>()
            .ForMember(dest => dest.IsRelevant, opt => opt.MapFrom(src => src.FeedbackRelevance.IsRelevant))
            .ForMember(dest => dest.Opinion, opt => opt.MapFrom(src => src.FeedbackOpinion.OverallOpinion))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.AnalysisResult.Status))
            .ForMember(
                dest => dest.Languages,
                opt => opt.MapFrom(src => src.FeedbackRelevance.RecognizedLanguages.Length != 0 ? src.FeedbackRelevance.RecognizedLanguages : null)
            )
            .ForMember(
                dest => dest.PositiveOpinionPoints,
                opt => opt.MapFrom(src => src.FeedbackOpinion.PositiveOpinionPoints.Length != 0 ? src.FeedbackOpinion.PositiveOpinionPoints : null)
            )
            .ForMember(
                dest => dest.NegativeOpinionPoints,
                opt => opt.MapFrom(src => src.FeedbackOpinion.NegativeOpinionPoints.Length != 0 ? src.FeedbackOpinion.NegativeOpinionPoints : null)
            )
            .ForMember(
                dest => dest.ModelExecutionDurationInMilliseconds,
                opt => opt.MapFrom(src => (ulong)src.AnalysisResult.ModelExecutionDuration.TotalMilliseconds)
            )
            .ForMember(
                dest => dest.AnalysisDurationInMilliseconds,
                opt => opt.MapFrom(src => (ulong)src.AnalysisResult.AnalysisDuration.TotalMilliseconds)
            );
    }
}