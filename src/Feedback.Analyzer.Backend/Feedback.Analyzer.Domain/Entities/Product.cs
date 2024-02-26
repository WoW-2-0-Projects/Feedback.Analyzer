using Feedback.Analyzer.Domain.Common.Entities;

namespace Feedback.Analyzer.Domain.Entities;

public class Product : AuditableEntity
{
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;
    
    public Guid OrganizationId { get; set; }

    public Organization Organization { get; set; } = default!;
}