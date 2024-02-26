using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(product => product.Name).IsRequired().HasMaxLength(128);
        builder.Property(product => product.Description).IsRequired().HasMaxLength(512);

        builder
            .HasOne(product => product.Organization)
            .WithMany(organization => organization.Products)
            .HasForeignKey(product => product.OrganizationId);
    }
}