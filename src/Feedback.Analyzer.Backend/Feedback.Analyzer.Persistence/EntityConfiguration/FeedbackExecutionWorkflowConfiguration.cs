using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class FeedbackExecutionWorkflowConfiguration : IEntityTypeConfiguration<FeedbackExecutionWorkflow>
{
    public void Configure(EntityTypeBuilder<FeedbackExecutionWorkflow> builder)
    {
        builder.HasOne(promptCategory => promptCategory.StartingExecutionOption)
            .WithOne(executionOptions => executionOptions.FeedbackExecutionWorkflow)
            .HasForeignKey<FeedbackExecutionWorkflow>(promptCategory => promptCategory.StartingExecutionOptionId);
    }
}