using AutoMapper;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Common.AnalysisWorkflows.Mappers;

public class AnalysisWorkflowMapper : Profile
{
    public AnalysisWorkflowMapper()
    {
        CreateMap<AnalysisWorkflow, FeedbackAnalysisWorkflowDto>().ReverseMap();
    }
}