using Feedback.Analyzer.Application.Organizations.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Organizations.Queries;

public class OrganizationGetQuery : IQuery<ICollection<OrganizationDto>>
{
    public OrganizationFilter OrganizationFilter { get; init; } = default!;
}