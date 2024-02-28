using Feedback.Analyzer.Application.Organizations.Models;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.Organizations.Commands;

/// <summary>
/// Represents a command to create a new organization. 
/// </summary>
public record OrganizationCreateCommand : ICommand<OrganizationDto>
{
    /// <summary>
    /// The data required to create a new organization.
    /// </summary>
    public OrganizationDto Organization { get; set; } = default!; 
}