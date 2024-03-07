using AutoMapper;
using Feedback.Analyzer.Application.AnalysisWorkflows.Models;
using Feedback.Analyzer.Application.AnalysisWorkflows.Queries;
using Feedback.Analyzer.Application.AnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.AnalysisWorkflows.QueryHandlers;

/// <summary>
/// Represents a query for retrieving analysis workflows.
/// </summary>
public class AnalysisWorkflowGetQueryHandler(IMapper mapper, IAnalysisWorkflowService analysisWorkflowService)
    : IQueryHandler<AnalysisWorkflowGetQuery, ICollection<AnalysisWorkflowDto>>
{
    public async Task<ICollection<AnalysisWorkflowDto>> Handle(AnalysisWorkflowGetQuery request, CancellationToken cancellationToken)
    {
        var matchedAnalysisWorkflow = await analysisWorkflowService
            .Get(request.Filter, new QueryOptions { AsNoTracking = true }).ToListAsync(cancellationToken);

        return mapper.Map<ICollection<AnalysisWorkflowDto>>(matchedAnalysisWorkflow);
    }
}