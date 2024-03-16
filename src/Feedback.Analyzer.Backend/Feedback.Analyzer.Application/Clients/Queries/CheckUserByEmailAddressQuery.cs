using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Clients.Queries;

/// <summary>
/// Represents user checking query that returns user's firstname if exists
/// </summary>
public class CheckUserByEmailAddressQuery : IQuery<string?>
{

    /// <summary>
    /// Gets user email address
    /// </summary>
    public string EmailAddress { get; set; } = default!;
}