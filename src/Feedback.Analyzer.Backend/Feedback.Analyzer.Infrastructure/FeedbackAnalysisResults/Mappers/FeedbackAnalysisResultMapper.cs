using AutoMapper;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisResults.Mappers;

/// <summary>
/// Mapper class for mapping between <see cref="FeedbackAnalysisResult"/> and <see cref="FeedbackAnalysisResultDto"/>.
/// </summary>
public class FeedbackAnalysisResultMapper : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FeedbackAnalysisResultMapper"/> class.
    /// </summary>
    public FeedbackAnalysisResultMapper()
    {
        CreateMap<FeedbackAnalysisResult, FeedbackAnalysisResultDto>().ReverseMap();
    }
}