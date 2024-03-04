using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Common.Prompts.Queries;

public record PromptResultGetByCategoryIdQuery : IQuery<ICollection<PromptResultDto>>
{
    public Guid CategoryId { get; set; }
}