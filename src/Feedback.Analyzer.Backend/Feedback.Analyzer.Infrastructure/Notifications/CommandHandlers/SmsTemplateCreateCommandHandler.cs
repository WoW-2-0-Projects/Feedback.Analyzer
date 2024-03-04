using AutoMapper;
using Feedback.Analyzer.Application.Notifications.Commands;
using Feedback.Analyzer.Application.Notifications.Models;
using Feedback.Analyzer.Application.Notifications.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Notifications.CommandHandlers;

/// <summary>
/// Handles the execution of the <see cref="SmsTemplateCreateCommand"/>.
/// Responsible for coordinating the creation of a new sms template.
/// </summary>
/// <param name="mapper"></param>
/// <param name="smsTemplateService"></param>
public class SmsTemplateCreateCommandHandler(
    IMapper mapper,
    ISmsTemplateService smsTemplateService) :
    ICommandHandler<SmsTemplateCreateCommand, SmsTemplateDto>
{
    public async Task<SmsTemplateDto> Handle(SmsTemplateCreateCommand request, CancellationToken cancellationToken)
    {
        // Conversion to domain entity cancellationToken
        var smsTemplate = mapper.Map<SmsTemplate>(request.SmsTemplateDto);

        // Call service
        var createdSmsTemplate = await smsTemplateService.CreateAsync(
            smsTemplate, cancellationToken: cancellationToken);

        // Conversion to DTO
        var smsTemplateDto = mapper.Map<SmsTemplateDto>(createdSmsTemplate);

        return smsTemplateDto;
    }
}