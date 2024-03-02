using AutoMapper;
using Feedback.Analyzer.Application.Notifications.Models;
using Feedback.Analyzer.Application.Notifications.Queries;
using Feedback.Analyzer.Application.Notifications.Services;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Infrastructure.Notifications.QueryHandlers;

/// <summary>
/// Handles the execution of the <see cref="EmailTemplateGetQuery"/>.
/// Responsible for retrieving email templates based on their filter.
/// </summary>
/// <param name="emailTemplateService"></param>
/// <param name="mapper"></param>
public class EmailTemplateGetQueryHandler(
    IEmailTemplateService emailTemplateService,
    IMapper mapper) :
    IQueryHandler<EmailTemplateGetQuery, ICollection<EmailTemplateDto>>
{
    public async Task<ICollection<EmailTemplateDto>> Handle(
    EmailTemplateGetQuery request,
    CancellationToken cancellationToken)
    {
        var matchedEmailTemplates = await emailTemplateService.Get(request.EmailTemplateFilter,
            new QueryOptions() { AsNoTracking = true })
            .ToListAsync(cancellationToken);

        return mapper.Map<ICollection<EmailTemplateDto>>(matchedEmailTemplates);
    }
}
