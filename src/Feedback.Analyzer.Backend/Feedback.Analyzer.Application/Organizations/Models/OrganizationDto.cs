using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Organizations.Models;

/// <summary>
/// Represents a data transfer object (DTO) for an organization.
/// This class will hold the properties needed to transfer organization-related data.
/// </summary>
public class OrganizationDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the organization.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// The unique identifier of the client associated with this organization.
    /// </summary>
    public Guid ClientId { get; set; }
    
    /// <summary>
    /// Gets or sets the  name of the Organization.
    /// </summary>
    public string Name { get; set; } = default!;
    
    /// <summary>
    ///  Gets or sets the  description of the Organization.
    /// </summary>
    public string Description { get; set; } = default!;

}