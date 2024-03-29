using Feedback.Analyzer.Application.Organizations.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Organizations.Queries;

/// <summary>
///  Represents a query to retrieve a collection of organizations.
/// </summary>
public record OrganizationGetQuery : IQuery<ICollection<OrganizationDto>>
{
    /// <summary>
    ///  Gets or sets the filter to apply when retrieving organizations.
    /// </summary>
    public OrganizationFilter Filter { get; set; } = default!;
}