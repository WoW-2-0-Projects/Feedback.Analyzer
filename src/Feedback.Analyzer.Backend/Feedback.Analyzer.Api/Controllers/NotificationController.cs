using Feedback.Analyzer.Application.Clients.Queries;
using Feedback.Analyzer.Application.Notifications.Commands;
using Feedback.Analyzer.Application.Notifications.Queries;
using Feedback.Analyzer.Application.Notifications.Services;
using Feedback.Analyzer.Domain.Common.Query;
using Feedback.Analyzer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Analyzer.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class NotificationController(
    IMediator mediator) : ControllerBase
{
    [HttpGet("templates/sms")]
    public async ValueTask<IActionResult> GetSmsTemplates(
        [FromQuery] SmsTemplateGetQuery smsTemplateGetQuery,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.Send(smsTemplateGetQuery, cancellationToken);
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("templates/email")]
    public async ValueTask<IActionResult> GetEmailTemplates(
        [FromQuery] EmailTemplateGetQuery emailTemplateGetQuery,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.Send(emailTemplateGetQuery, cancellationToken);
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpPost("templates/sms")]
    public async ValueTask<IActionResult> CreateSmsTemplate(
        [FromBody] SmsTemplateCreateCommand smsTemplateCreateCommand,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.Send(smsTemplateCreateCommand, cancellationToken);
        return result is not null ? Ok(result) : NoContent();
    }

    [HttpPost("templates/email")]
    public async ValueTask<IActionResult> CreateEmailTemplate(
        [FromBody] EmailTemplateCreateCommand emailTemplateCreateCommand,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.Send(emailTemplateCreateCommand, cancellationToken);
        return result is not null ? Ok(result) : NoContent();
    }
}
