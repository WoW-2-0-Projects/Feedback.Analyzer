using Feedback.Analyzer.Api.Models.DTOs;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Organizations.Queries;

public class OrganizationGetQuery : IQuery<ICollection<OrganizationDto>>
{
    public OrganizationFilter OrganizationFilter { get; init; } = default!;
}