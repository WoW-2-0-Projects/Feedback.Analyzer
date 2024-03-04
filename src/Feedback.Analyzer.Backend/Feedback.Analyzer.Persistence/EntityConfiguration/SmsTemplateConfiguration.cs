using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class SmsTemplateConfiguration : IEntityTypeConfiguration<SmsTemplate>
{
    public void Configure(EntityTypeBuilder<SmsTemplate> builder)
    {
        
    }
}
