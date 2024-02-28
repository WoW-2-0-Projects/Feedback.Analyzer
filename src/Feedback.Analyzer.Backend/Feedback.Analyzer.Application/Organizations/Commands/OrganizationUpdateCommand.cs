using Feedback.Analyzer.Application.Organizations.Models;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.Organizations.Commands;

/// <summary>
/// Represents a command to modify an existing organization's information.
/// </summary>
public record OrganizationUpdateCommand : ICommand<OrganizationDto>
{
    /// <summary>
    /// Contains the updated organization data. 
    /// </summary>
    public OrganizationDto Organization { get; set; } = default!; 
}