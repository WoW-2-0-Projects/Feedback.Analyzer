using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Repository implementation for managing sms template entities
/// </summary>
/// <param name="dbContext"></param>
public class SmsTemplateRepository(AppDbContext dbContext) : 
    EntityRepositoryBase<SmsTemplate, AppDbContext>(dbContext),
    ISmsTemplateRepository
{
    public IQueryable<SmsTemplate> Get(
        Expression<Func<SmsTemplate, bool>>? predicate, 
        QueryOptions queryOptions = default) =>
    base.Get(predicate, queryOptions);

    public ValueTask<SmsTemplate> CreateAsync(
        SmsTemplate smsTemplate, 
        CommandOptions commandOptions = default, 
        CancellationToken cancellationToken = default) =>
    base.CreateAsync(smsTemplate, commandOptions, cancellationToken);
}
