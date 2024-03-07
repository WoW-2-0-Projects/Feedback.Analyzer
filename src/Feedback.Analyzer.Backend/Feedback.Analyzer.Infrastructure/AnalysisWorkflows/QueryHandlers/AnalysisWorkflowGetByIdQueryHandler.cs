using AutoMapper;
using Feedback.Analyzer.Application.AnalysisWorkflows.Models;
using Feedback.Analyzer.Application.AnalysisWorkflows.Queries;
using Feedback.Analyzer.Application.AnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Infrastructure.AnalysisWorkflows.QueryHandlers;

/// <summary>
/// Handles the query for retrieving an analysis workflow by its ID.
/// </summary>
public class AnalysisWorkflowGetByIdQueryHandler(IAnalysisWorkflowService analysisWorkflowService, IMapper mapper)
    : IQueryHandler<AnalysisWorkflowGetByIdQuery, AnalysisWorkflowDto>
{
    public async Task<AnalysisWorkflowDto> Handle(AnalysisWorkflowGetByIdQuery request, CancellationToken cancellationToken)
    {
        var foundAnalysisWorkflow = await analysisWorkflowService.GetByIdAsync(
            request.AnalysisWorkflowId,
            new QueryOptions
            {
                AsNoTracking = true
            }, cancellationToken);

        return mapper.Map<AnalysisWorkflowDto>(foundAnalysisWorkflow);
    }
}