using Feedback.Analyzer.Application.Organizations.Models;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Organizations.Queries;

/// <summary>
/// Represents a query to retrieve a specific organization by its ID.
/// </summary>
public class OrganizationGetByIdQuery : IQuery<OrganizationDto?>
{
    /// <summary>
    /// The unique identifier of the organization to retrieve.
    /// </summary>
    public Guid OrganizationId { get; set; } = Guid.Empty;
}