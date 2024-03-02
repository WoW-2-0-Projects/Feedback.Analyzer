using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class NotificationTemplateConfiguration : IEntityTypeConfiguration<NotificationTemplate>
{
    public void Configure(EntityTypeBuilder<NotificationTemplate> builder)
    {
        builder
            .Property(template => template.Content)
            .IsRequired()
            .HasMaxLength(129_536);

        builder
            .HasIndex(template => new
            {
                template.Type,
                template.TemplateType
            })
            .IsUnique();

        builder
            .ToTable("NotificationTemplates")
            .HasDiscriminator(template => template.Type)
            .HasValue<EmailTemplate>(NotificationType.Email)
            .HasValue<SmsTemplate>(NotificationType.Sms);
    }
}
