using AutoMapper;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Commands;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Models;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisResults.CommandHandlers;

/// <summary>
/// Command handler for creating feedback analysis results.
/// </summary>
public class FeedbackAnalysisResultCreateCommandHandler(IMapper mapper, IFeedbackAnalysisResultService feedbackAnalysisResultService) : ICommandHandler<FeedbackAnalysisResultCreateCommand, FeedbackAnalysisResultDto>
{
    public async Task<FeedbackAnalysisResultDto> Handle(FeedbackAnalysisResultCreateCommand request, CancellationToken cancellationToken)
    {
        var feedbackAnalysisResult = mapper.Map<FeedbackAnalysisResult>(request.FeedbackAnalysisResult);

        var createFeedbackAnalysisResult =
            await feedbackAnalysisResultService.CreateAsync(feedbackAnalysisResult,
                cancellationToken: cancellationToken);

        var feedbackAnalysisResultDto = mapper.Map<FeedbackAnalysisResultDto>(createFeedbackAnalysisResult);

        return feedbackAnalysisResultDto;
    }
}