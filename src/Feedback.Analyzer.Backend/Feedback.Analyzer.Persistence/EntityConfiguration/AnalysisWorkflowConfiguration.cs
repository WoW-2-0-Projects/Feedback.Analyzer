using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class AnalysisWorkflowConfiguration : IEntityTypeConfiguration<AnalysisWorkflow>
{
    public void Configure(EntityTypeBuilder<AnalysisWorkflow> builder)
    {
        builder
            .Property(workflow => workflow.Name)
            .HasMaxLength(128)
            .IsRequired();
        
        builder
            .HasOne(workflow => workflow.EntryExecutionOption)
            .WithOne(executionOptions => executionOptions.Workflow)
            .HasForeignKey<AnalysisWorkflow>(executionOptions => executionOptions.EntryExecutionOptionId);
    }
}