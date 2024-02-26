using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(organization => organization.Name).IsRequired().HasMaxLength(128);
        builder.Property(organization => organization.Description).IsRequired().HasMaxLength(512);

        builder
            .HasOne(product => product.Organization)
            .WithMany(organization => organization.Products)
            .HasForeignKey(product => product.OrganizationId);
    }
}