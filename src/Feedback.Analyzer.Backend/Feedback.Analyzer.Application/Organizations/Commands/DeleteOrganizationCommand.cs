using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.Organizations.Commands;

/// <summary>
/// Represents a command to delete an existing organization.
/// </summary>
public class DeleteOrganizationByIdCommand : ICommand<bool>
{
    /// <summary>
    /// The unique identifier of the organization to be deleted.
    /// </summary>
    public Guid OrganizationId { get; init; }
}