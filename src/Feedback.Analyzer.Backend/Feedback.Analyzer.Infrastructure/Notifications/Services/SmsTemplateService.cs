using Feedback.Analyzer.Application.Notifications.Models;
using Feedback.Analyzer.Application.Notifications.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Infrastructure.Notifications.Validators;
using Feedback.Analyzer.Persistence.Extensions;
using Feedback.Analyzer.Persistence.Repositories;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;
using System.Linq.Expressions;

namespace Feedback.Analyzer.Infrastructure.Notifications.Services;

public class SmsTemplateService(
    ISmsTemplateRepository smsTemplateRepository,
    IValidator<SmsTemplate> smsTemplateValidator) :
    ISmsTemplateService
{
    public IQueryable<SmsTemplate> Get(
        Expression<Func<SmsTemplate, bool>>? predicate, 
        QueryOptions queryOptions = default) =>
    smsTemplateRepository.Get(predicate, queryOptions);

    public IQueryable<SmsTemplate> Get(
        SmsTemplateFilter smsTemplateFilter,
        QueryOptions queryOptions = default) =>
    smsTemplateRepository.Get().ApplyPagination(smsTemplateFilter);

    public ValueTask<SmsTemplate> CreateAsync(
    SmsTemplate smsTemplate, 
        CommandOptions commandOptions = default, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = smsTemplateValidator
            .Validate(smsTemplate,
                options =>
                    options.IncludeRuleSets(EntityEvent.OnCreate.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return smsTemplateRepository.CreateAsync(smsTemplate, commandOptions, cancellationToken);
    }
}
