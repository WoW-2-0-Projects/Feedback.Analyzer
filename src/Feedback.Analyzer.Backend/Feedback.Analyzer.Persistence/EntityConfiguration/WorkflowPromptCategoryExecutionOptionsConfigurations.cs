using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class WorkflowPromptCategoryExecutionOptionsConfigurations : IEntityTypeConfiguration<WorkflowExecutionOptions>
{
    public void Configure(EntityTypeBuilder<WorkflowExecutionOptions> builder)
    {
        // builder.HasKey(
        //     executionOptions => new
        //     {
        //         executionOptions.AnalysisPromptCategoryId,
        //         executionOptions.FeedbackExecutionWorkflowId
        //     }
        // );

        builder.HasOne(executionOptions => executionOptions.AnalysisPromptCategory)
            .WithMany(promptCategory => promptCategory.FeedbackWorkflowExecutionOptions)
            .HasForeignKey(pc => pc.AnalysisPromptCategoryId);

        builder.HasOne<WorkflowExecutionOptions>()
            .WithMany(executionOptions => executionOptions.ChildExecutionOptions)
            .HasForeignKey(executionOptions => executionOptions.ParentId)
            //     executionOptions => new
            //     { 
            //         executionOptions.AnalysisPromptCategoryId,
            //         executionOptions.FeedbackExecutionWorkflowId
            //     }
            // )
            .IsRequired(false);
    }
}