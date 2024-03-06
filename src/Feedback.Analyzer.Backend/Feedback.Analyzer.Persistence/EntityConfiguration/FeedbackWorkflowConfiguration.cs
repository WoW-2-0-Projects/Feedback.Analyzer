using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class FeedbackWorkflowConfiguration : IEntityTypeConfiguration<FeedbackWorkflow>
{
    public void Configure(EntityTypeBuilder<FeedbackWorkflow> builder)
    {
        builder.Property(workflow => workflow.Name).HasMaxLength(128).IsRequired();

        builder.HasIndex(
            workflow => new
            {
                workflow.Name,
                workflow.ProductId
            }
        ).IsUnique();
    }
}