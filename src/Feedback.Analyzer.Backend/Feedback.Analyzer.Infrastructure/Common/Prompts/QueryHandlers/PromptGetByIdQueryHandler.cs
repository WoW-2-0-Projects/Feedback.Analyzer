using AutoMapper;
using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Application.Common.Prompts.Queries;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Infrastructure.Common.Prompts.QueryHandlers;

public class PromptGetByIdQueryHandler(IPromptService promptService, IMapper mapper) : IQueryHandler<PromptGetByIdQuery, AnalysisPromptDto>
{
    public async Task<AnalysisPromptDto> Handle(PromptGetByIdQuery request, CancellationToken cancellationToken)
    {
        var foundPrompt = await promptService.GetByIdAsync(
            request.PromptId,
            new QueryOptions
            {
                TrackingMode = QueryTrackingMode.AsNoTracking
            },
            cancellationToken
        );
        
        return mapper.Map<AnalysisPromptDto>(foundPrompt);
    }
}