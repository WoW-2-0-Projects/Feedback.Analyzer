using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class WorkflowPromptCategoryExecutionOptionsConfigurations : IEntityTypeConfiguration<WorkflowPromptCategoryExecutionOptions>
{
    public void Configure(EntityTypeBuilder<WorkflowPromptCategoryExecutionOptions> builder)
    {
        builder.Ignore(executionOptions => executionOptions.Id);
        
        builder.HasKey(
            executionOptions => new
            {
                executionOptions.AnalysisPromptCategoryId,
                executionOptions.FeedbackExecutionWorkflowId
            }
        );

        builder.HasOne(executionOptions => executionOptions.AnalysisPromptCategory)
            .WithMany(promptCategory => promptCategory.FeedbackWorkflowExecutionOptions)
            .HasForeignKey(pc => pc.AnalysisPromptCategoryId);

        builder.HasOne(executionOptions => executionOptions.FeedbackExecutionWorkflow)
            .WithMany(promptCategory => promptCategory.FeedbackWorkflowExecutionOptions)
            .HasForeignKey(pc => pc.FeedbackExecutionWorkflowId);

        builder.HasOne<WorkflowPromptCategoryExecutionOptions>()
            .WithMany(executionOptions => executionOptions.ChildExecutionOptions)
            .HasForeignKey(
                executionOptions => new
                {
                    executionOptions.AnalysisPromptCategoryId,
                    executionOptions.FeedbackExecutionWorkflowId
                }
            )
            .IsRequired(false);
    }
}