using AutoMapper;
using Feedback.Analyzer.Application.AnalysisWorkflows.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.AnalysisWorkflows.Mappers;

public class AnalysisWorkflowMapper : Profile
{
    public AnalysisWorkflowMapper()
    {
        CreateMap<AnalysisWorkflow, AnalysisWorkflowDto>().ReverseMap();
    }
}