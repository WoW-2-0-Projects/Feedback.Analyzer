using Feedback.Analyzer.Application.Notifications.Models;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using System.Linq.Expressions;

namespace Feedback.Analyzer.Application.Notifications.Services;

/// <summary>
/// Represents a service for managing email template entities.
/// </summary>
public interface IEmailTemplateService
{
    /// <summary>
    /// Retrieves a queryable collection of email template entities based on the specified predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="queryOptions"></param>
    /// <returns>A queryable collection of email template entities </returns>
    IQueryable<EmailTemplate> Get(
        Expression<Func<EmailTemplate, bool>>? predicate,
        QueryOptions queryOptions = default);

    /// <summary>
    /// Retrieves a filtered and potentially paginated queryable collection of email template.
    /// </summary>
    /// <param name="emailTemplateFilter"></param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    IQueryable<EmailTemplate> Get(
        EmailTemplateFilter emailTemplateFilter,
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
