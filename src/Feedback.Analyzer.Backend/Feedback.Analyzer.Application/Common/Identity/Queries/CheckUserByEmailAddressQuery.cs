using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Common.Identity.Queries;

/// <summary>
/// Represents user checking query that returns user's firstname if exists
/// </summary>
public record CheckClientByEmailAddressQuery : IQuery<string?>
{
    /// <summary>
    /// Gets user email address
    /// </summary>
    public string EmailAddress { get; init; } = default!;
}