using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using System.Linq.Expressions;

namespace Feedback.Analyzer.Persistence.Repositories.Interfaces;

/// <summary>
/// Represents a repository for managing sms template entities
/// </summary>
public interface ISmsTemplateRepository
{
    /// <summary>
    /// Retrieves a queryable collection of sms template entities based on the specified predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="queryOptions"></param>
    /// <returns>A queryable collection of sms template entities </returns>
    IQueryable<SmsTemplate> Get(
        Expression<Func<SmsTemplate, bool>>? predicate = default,
        QueryOptions queryOptions = default);

    /// <summary>
    /// Creates a new sms template
    /// </summary>
    /// <param name="smsTemplate"></param>
    /// <param name="commandOptions"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The new created sms template object</returns>
    ValueTask<SmsTemplate> CreateAsync(
        SmsTemplate smsTemplate,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}
