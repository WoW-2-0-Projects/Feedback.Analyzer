using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.Property(organization => organization.Name).IsRequired().HasMaxLength(128);
        builder.Property(organization => organization.Description).IsRequired().HasMaxLength(8192);

        builder
            .HasOne(organization => organization.Client)
            .WithMany(client => client.Organizations)
            .HasForeignKey(organization => organization.ClientId);
    }
}