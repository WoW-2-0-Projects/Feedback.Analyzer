using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Feedback.Analyzer.Persistence.Repositories;

/// <summary>
/// Repository implementation for managing email template entities
/// </summary>
/// <param name="dbContext"></param>
public class EmailTemplateRepository(AppDbContext dbContext) : 
    EntityRepositoryBase<EmailTemplate, AppDbContext>(dbContext),
    IEmailTemplateRepository
{
    public IQueryable<EmailTemplate> Get(
        Expression<Func<EmailTemplate, bool>>? predicate, 
        QueryOptions queryOptions = default) =>
    base.Get(predicate, queryOptions);

    public ValueTask<EmailTemplate> CreateAsync(
        EmailTemplate emailTemplate, 
        CommandOptions commandOptions = default, 
        CancellationToken cancellationToken = default) =>
    base.CreateAsync(emailTemplate, commandOptions, cancellationToken);
}
