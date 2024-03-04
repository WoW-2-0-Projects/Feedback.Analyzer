using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class EmailTemplateConfiuguration : IEntityTypeConfiguration<EmailTemplate>
{
    public void Configure(EntityTypeBuilder<EmailTemplate> builder)
    {
        builder
            .Property(template => template.Subject)
            .IsRequired()
            .HasMaxLength(256);
    }
}
