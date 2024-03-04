using AutoMapper;
using Feedback.Analyzer.Application.Notifications.Models;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Notifications.Mappers;

public class NotificationTemplateMapper : Profile
{
    public NotificationTemplateMapper()
    {
        CreateMap<EmailTemplate, EmailTemplateDto>().ReverseMap();

        CreateMap<SmsTemplate, SmsTemplateDto>().ReverseMap();
    }
}
