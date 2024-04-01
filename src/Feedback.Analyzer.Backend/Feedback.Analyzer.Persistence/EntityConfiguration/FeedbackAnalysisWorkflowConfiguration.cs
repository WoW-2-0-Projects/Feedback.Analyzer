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
        builder
            .HasOne(feedbackAnalysisWorkflow => feedbackAnalysisWorkflow.AnalysisWorkflow)
            .WithOne()
            .HasForeignKey<FeedbackAnalysisWorkflow>(feedbackAnalysisWorkflow => feedbackAnalysisWorkflow.Id);
        
        // TODO : Add name and product Id unique index between this and AnalysisWorkflow table
        
        builder
            .HasOne(workflow => workflow.Product)
            .WithMany(feedbackAnalysisWorkflow => feedbackAnalysisWorkflow.Workflows)
            .HasForeignKey(workflow => workflow.ProductId);
    }
}