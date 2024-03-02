using Feedback.Analyzer.Application.Notifications.Models;
using Feedback.Analyzer.Application.Notifications.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Persistence.Extensions;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;
using System.Linq.Expressions;

namespace Feedback.Analyzer.Infrastructure.Notifications.Services;

public class EmailTemplateService(
    IEmailTemplateRepository emailTemplateRepository,
    IValidator<EmailTemplate> emailTemplateValidator) :
    IEmailTemplateService
{
    public IQueryable<EmailTemplate> Get(
        Expression<Func<EmailTemplate, bool>>? predicate, 
        QueryOptions queryOptions = default) =>
    emailTemplateRepository.Get(predicate, queryOptions);

    public IQueryable<EmailTemplate> Get(
        EmailTemplateFilter emailTemplateFilter,
        QueryOptions queryOptions = default) =>
    emailTemplateRepository.Get().ApplyPagination(emailTemplateFilter);

    public ValueTask<EmailTemplate> CreateAsync(
    EmailTemplate emailTemplate, 
        CommandOptions commandOptions = default, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = emailTemplateValidator
            .Validate(emailTemplate,
                options =>
                    options.IncludeRuleSets(EntityEvent.OnCreate.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return emailTemplateRepository.CreateAsync(emailTemplate, commandOptions, cancellationToken);
    }
}
