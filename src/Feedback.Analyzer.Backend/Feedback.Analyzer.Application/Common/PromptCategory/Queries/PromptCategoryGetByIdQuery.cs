using Feedback.Analyzer.Application.Common.PromptCategory.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Common.PromptCategory.Queries;

public class PromptCategoryGetByIdQuery : IQuery<AnalysisPromptCategoryDto?>
{
    public Guid CategoryId { get; set; }
}