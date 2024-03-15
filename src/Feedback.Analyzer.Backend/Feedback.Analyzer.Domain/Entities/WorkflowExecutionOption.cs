using System.ComponentModel.DataAnnotations.Schema;
using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents an option for executing a workflow.
/// </summary>
public class WorkflowExecutionOption : Entity
{
    /// <summary>
    /// Gets or sets the unique identifier of the analysis prompt category associated with this execution option.
    /// </summary>
    public Guid AnalysisPromptCategoryId { get; set; }

    /// <summary>
    /// Gets or sets the analysis prompt category added to this execution option.
    /// </summary>
    public AnalysisPromptCategory AnalysisPromptCategory { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets a value indicating whether this execution option is disabled.
    /// </summary>
    public bool IsDisabled { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier of the parent execution option if applicable.
    /// </summary>
    public Guid? ParentId { get; set; }
    
    /// <summary>
    /// Gets or sets the list of child execution options if applicable.
    /// </summary>
    public IReadOnlyList<WorkflowExecutionOption>? ChildExecutionOptions { get; set; }
}
