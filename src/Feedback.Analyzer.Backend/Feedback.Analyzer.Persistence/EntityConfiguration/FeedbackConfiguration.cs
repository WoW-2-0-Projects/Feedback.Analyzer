using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class FeedbackConfiguration : IEntityTypeConfiguration<CustomerFeedback>
{
    public void Configure(EntityTypeBuilder<CustomerFeedback> builder)
    {
        builder.Property(feedback => feedback.UserName).IsRequired().HasMaxLength(64);
        builder.Property(feedback => feedback.Comment).IsRequired().HasMaxLength(2048);

        builder
            .HasOne(feedback => feedback.Product)
            .WithMany(product => product.CustomerFeedbacks)
            .HasForeignKey(feedback => feedback.ProductId);
    }
}