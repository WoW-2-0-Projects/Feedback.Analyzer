using AutoMapper;
using Feedback.Analyzer.Application.Notifications.Commands;
using Feedback.Analyzer.Application.Notifications.Models;
using Feedback.Analyzer.Application.Notifications.Services;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Infrastructure.Notifications.CommandHandlers;

/// <summary>
/// Handles the execution of the <see cref="EmailTemplateCreateCommand"/>.
/// Responsible for coordinating the creation of a new email template.
/// </summary>
/// <param name="mapper"></param>
/// <param name="emailTemplateService"></param>
public class EmailTemplateCreateCommandHandler(
    IMapper mapper,
    IEmailTemplateService emailTemplateService) :
    ICommandHandler<EmailTemplateCreateCommand, EmailTemplateDto>
{
    public async Task<EmailTemplateDto> Handle(EmailTemplateCreateCommand request, CancellationToken cancellationToken)
    {
        // Conversion to domain entity cancellationToken
        var emailTemplate = mapper.Map<EmailTemplate>(request.EmailTemplateDto);

        // Call service
        var createdEmailTemplate = await emailTemplateService.CreateAsync(
            emailTemplate, cancellationToken: cancellationToken);

        // Conversion to DTO
        var emailTemplateDto = mapper.Map<EmailTemplateDto>(createdEmailTemplate);

        return emailTemplateDto;
    }
}
