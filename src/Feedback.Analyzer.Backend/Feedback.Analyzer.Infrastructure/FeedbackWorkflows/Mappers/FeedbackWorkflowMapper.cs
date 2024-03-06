using AutoMapper;
using Feedback.Analyzer.Application.FeedbackWorkflows.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.FeedbackWorkflows.Mappers;

public class FeedbackWorkflowMapper : Profile
{
    public FeedbackWorkflowMapper()
    {
        CreateMap<FeedbackWorkflow, FeedbackWorkflowDto>().ReverseMap();
    }
}