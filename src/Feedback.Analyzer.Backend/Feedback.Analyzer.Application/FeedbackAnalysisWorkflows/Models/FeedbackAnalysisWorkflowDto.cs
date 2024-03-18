using Feedback.Analyzer.Domain.Common.Entities;
using Feedback.Analyzer.Domain.Enums;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Models;

/// <summary>
/// Represents a data transfer object (DTO) for feedback analysis workflow
/// </summary>
public class FeedbackAnalysisWorkflowDto 
{
    /// <summary>
    /// Gets or sets the unique identifier of the entity.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the Id of product associated with the feedback analysis workflow
    /// </summary>
    public Guid ProductId { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the feedback analysis workflow.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    public string ProductName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the type of the feedback analysis workflow.
    /// </summary>
    public WorkflowType Type { get; set; }

}