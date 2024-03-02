using AutoMapper;
using Feedback.Analyzer.Application.Notifications.Models;
using Feedback.Analyzer.Application.Notifications.Queries;
using Feedback.Analyzer.Application.Notifications.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Notifications.QueryHandlers;

/// <summary>
/// Handles the execution of the <see cref="SmsTemplateGetQuery"/>.
/// Responsible for retrieving sms templates based on their filter.
/// </summary>
/// <param name="smsTemplateService"></param>
/// <param name="mapper"></param>
public class SmsTemplateGetQueryHandler(
    ISmsTemplateService smsTemplateService,
    IMapper mapper) :
    IQueryHandler<SmsTemplateGetQuery, ICollection<SmsTemplateDto>>
{
    public async Task<ICollection<SmsTemplateDto>> Handle(
    SmsTemplateGetQuery request,
    CancellationToken cancellationToken)
    {
        var matchedSmsTemplates = await smsTemplateService.Get(request.SmsTemplateFilter,
            new QueryOptions() { AsNoTracking = true })
            .ToListAsync(cancellationToken);

        return mapper.Map<ICollection<SmsTemplateDto>>(matchedSmsTemplates);
    }
}
