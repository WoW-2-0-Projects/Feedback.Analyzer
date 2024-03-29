using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

/// <summary>
/// Represents an organization within the system.
/// </summary>
public class Organization : SoftDeletedEntity
{
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

    /// <summary>
    /// Gets or sets a collection of Clients.
    /// </summary>
    public Client Client { get; set; } = default!;

    /// <summary>
    /// Gets or sets the collection of products.
    /// </summary>
    public IEnumerable<Product> Products { get; set; } = default!;
}