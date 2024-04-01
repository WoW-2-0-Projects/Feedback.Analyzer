using Feedback.Analyzer.Application.Organizations.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Models;

public class FeedbackAnalysisWorkflowResultFilter : FilterPagination
{
    /// <summary>
    /// Gets or sets workflow id
    /// </summary>
    public Guid? WorkflowId { get; set; }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        hashCode.Add(PageSize);
        hashCode.Add(PageToken);

        return hashCode.ToHashCode();
    }

    public override bool Equals(object? obj) => 
        obj is OrganizationFilter clientFilter 
        && clientFilter.GetHashCode() == GetHashCode();
}