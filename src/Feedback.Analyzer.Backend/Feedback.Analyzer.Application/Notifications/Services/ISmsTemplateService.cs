using Feedback.Analyzer.Application.Notifications.Models;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using System.Linq.Expressions;

namespace Feedback.Analyzer.Application.Notifications.Services;

/// <summary>
/// Represents a service for managing sms template entities.
/// </summary>
public interface ISmsTemplateService
{
    /// <summary>
    /// Retrieves a queryable collection of sms template entities based on the specified predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="queryOptions"></param>
    /// <returns>A queryable collection of sms template entities </returns>
    IQueryable<SmsTemplate> Get(
        Expression<Func<SmsTemplate, bool>>? predicate,
        QueryOptions queryOptions = default);

    /// <summary>
    /// Retrieves a filtered and potentially paginated queryable collection of sms template.
    /// </summary>
    /// <param name="smsTemplateFilter"></param>
    /// <param name="queryOptions"></param>
    /// <returns></returns>
    IQueryable<SmsTemplate> Get(
        SmsTemplateFilter smsTemplateFilter,
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
