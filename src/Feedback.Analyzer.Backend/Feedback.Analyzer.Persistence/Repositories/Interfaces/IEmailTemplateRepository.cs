using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using System.Linq.Expressions;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

/// <summary>
/// Represents a repository for managing email template entities
/// </summary>
public interface IEmailTemplateRepository
{
    /// <summary>
    /// Retrieves a queryable collection of email template entities based on the specified predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="queryOptions"></param>
    /// <returns>A queryable collection of email template entities </returns>
    IQueryable<EmailTemplate> Get(
        Expression<Func<EmailTemplate, bool>>? predicate = default,
        QueryOptions queryOptions = default);

    /// <summary>
    /// Creates a new email template
    /// </summary>
    /// <param name="emailTemplate"></param>
    /// <param name="commandOptions"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The new created email template object</returns>
    ValueTask<EmailTemplate> CreateAsync(
        EmailTemplate emailTemplate,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}
