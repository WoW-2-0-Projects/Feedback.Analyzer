using Feedback.Analyzer.Domain.Common.Entities;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents an analysis workflow entity.
/// </summary>
public class AnalysisWorkflow : AuditableEntity
{
    /// <summary>
    /// Gets or sets the name of the analysis workflow.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the type of the analysis workflow.
    /// </summary>
    public WorkflowType Type { get; set; }

    /// <summary>
    /// Gets or sets entry execution options Id 
    /// </summary>
    public Guid EntryExecutionOptionId { get; set; }
    
    /// <summary>
    /// Gets or sets the running status of the analysis workflow
    /// </summary>
    public WorkflowStatus Status { get; set; }
    
    /// <summary>
    /// Gets or sets entry execution options
    /// </summary>
    public WorkflowExecutionOption EntryExecutionOption { get; set; } = default!;
}
