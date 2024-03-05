using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class WorkflowPromptCategoryExecutionOptionsConfigurations : IEntityTypeConfiguration<WorkflowPromptCategoryExecutionOptions>
{
    public void Configure(EntityTypeBuilder<WorkflowPromptCategoryExecutionOptions> builder)
    {
        builder.HasKey(
                pc => new
                {
                    pc.AnalysisPromptCategoryId,
                    pc.FeedbackExecutionWorkflowId
                }
            );

        builder
            .HasOne(executionOptions => executionOptions.AnalysisPromptCategory)
            .WithMany(promptCategory => promptCategory.FeedbackWorkflowExecutionOptions)
            .HasForeignKey(pc => pc.AnalysisPromptCategoryId);
        
        builder
            .HasOne(executionOptions => executionOptions.FeedbackExecutionWorkflow)
            .WithMany(promptCategory => promptCategory.FeedbackWorkflowExecutionOptions)
            .HasForeignKey(pc => pc.FeedbackExecutionWorkflowId);
    }
}