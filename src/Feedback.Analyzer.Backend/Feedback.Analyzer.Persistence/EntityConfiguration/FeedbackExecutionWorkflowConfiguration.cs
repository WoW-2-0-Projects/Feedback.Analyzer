using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class FeedbackExecutionWorkflowConfiguration : IEntityTypeConfiguration<FeedbackAnalysisWorkflow>
{
    public void Configure(EntityTypeBuilder<FeedbackAnalysisWorkflow> builder)
    {
        builder
            .Property(workflow => workflow.Name)
            .HasMaxLength(128)
            .IsRequired();
        
        builder
            .HasOne(workflow => workflow.Product)
            .WithMany()
            .HasForeignKey(workflow => workflow.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(workflow => workflow.StartingExecutionOption)
            .WithOne(executionOptions => executionOptions.FeedbackAnalysisWorkflow)
            .HasForeignKey<FeedbackAnalysisWorkflow>(promptCategory => promptCategory.StartingExecutionOptionId);

        builder
            .HasIndex(
                workflow => new
                {
                    workflow.Name,
                    workflow.ProductId
                }
            )
            .IsUnique();
    }
}