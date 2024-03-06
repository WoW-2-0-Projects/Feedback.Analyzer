using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.Common.PromptCategories.Commands;

public class UpdateSelectedPromptCommand : ICommand<bool>
{
    public Guid CategoryId { get; set; }
    
    public Guid PromptId { get; set; }
}