using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class FeedbackAnalysisWorkflowResultStatsConfiguration : IEntityTypeConfiguration<FeedbackAnalysisWorkflowResultStats>
{
    public void Configure(EntityTypeBuilder<FeedbackAnalysisWorkflowResultStats> builder)
    {
        builder
            .Property(stats => stats.Label)
            .HasMaxLength(64)
            .IsRequired();
        
        builder
            .Property(stats => stats.Count)
            .HasColumnType("integer");
        
        builder
            .Property(stats => stats.Percentage)
            .HasConversion(v => (short)(v * 10), v => (float)v / 10)
            .HasColumnType("smallint");
        
        builder
            .HasOne<FeedbackAnalysisWorkflowResult>()
            .WithMany(workflowResult => workflowResult.Statistics)
            .HasForeignKey(stats => stats.WorkflowResultId);
    }
}