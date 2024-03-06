using Feedback.Analyzer.Application.Common.PromptCategories.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Common.PromptCategories.Queries;

public class PromptCategoryGetByIdQuery: IQuery<AnalysisPromptCategoryDto?>
{
    public Guid CategoryId { get; set; }
}