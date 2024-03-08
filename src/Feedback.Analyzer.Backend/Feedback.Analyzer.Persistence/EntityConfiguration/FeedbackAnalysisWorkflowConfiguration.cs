using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

/// <summary>
/// Represents the configuration for feedback analysis workflows.
/// </summary>
public class FeedbackAnalysisWorkflowConfiguration : IEntityTypeConfiguration<FeedbackAnalysisWorkflow>
{
    public void Configure(EntityTypeBuilder<FeedbackAnalysisWorkflow> builder)
    {
        builder.Property(workflow => workflow.Name).HasMaxLength(128).IsRequired();
        
        builder.Property(analysisWorkflow => analysisWorkflow.Type)
            .HasConversion<string>()
            .HasMaxLength(128)
            .IsRequired();
        
        builder.HasIndex(
            workflow => new
            {
                workflow.Name,
                workflow.ProductId
            }
        ).IsUnique();
       
        builder
            .HasOne(workflow => workflow.Product)
            .WithMany(feedbackAnalysisWorkflow => feedbackAnalysisWorkflow.Workflows)
            .HasForeignKey(workflow => workflow.ProductId);
    }
}