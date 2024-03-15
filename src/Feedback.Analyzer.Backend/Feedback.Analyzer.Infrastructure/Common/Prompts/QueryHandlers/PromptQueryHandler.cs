using AutoMapper;
using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Application.Common.Prompts.Queries;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Common.Prompts.QueryHandlers;

/// <summary>
/// Handles the execution of the <see cref="PromptGetQuery"/>.
/// Responsible for retrieving a specific prompt based on filter
/// </summary>
public class PromptQueryHandler(IPromptService promptService, IMapper mapper) : IQueryHandler<PromptGetQuery, ICollection<AnalysisPromptDto>>
{
    public async Task<ICollection<AnalysisPromptDto>> Handle(PromptGetQuery request, CancellationToken cancellationToken)
    {
        var matchedPrompts =  await promptService.Get(request.Filter, new QueryOptions
            { TrackingMode = QueryTrackingMode.AsNoTracking }).ToListAsync(cancellationToken);
        
        return mapper.Map<ICollection<AnalysisPromptDto>>(matchedPrompts);
    }
}