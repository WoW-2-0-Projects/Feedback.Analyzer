using AutoMapper;
using Feedback.Analyzer.Application.Common.Workflows.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Common.Workflows.Mappers;

public class FeedbackExecutionWorkflowMapper : Profile
{
    public FeedbackExecutionWorkflowMapper()
    {
        CreateMap<FeedbackExecutionWorkflow, FeedbackExecutionWorkflowDto>().ReverseMap();
    }
}