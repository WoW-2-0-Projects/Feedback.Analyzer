using AutoMapper;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Mappers;

public class FeedbackAnalysisWorkflowMapper : Profile
{
    public FeedbackAnalysisWorkflowMapper()
    {
        CreateMap<FeedbackAnalysisWorkflow, FeedbackAnalysisWorkflowDto>().ReverseMap();
    }
}