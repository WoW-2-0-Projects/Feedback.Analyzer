using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class WorkflowExecutionOptionConfiguration : IEntityTypeConfiguration<WorkflowExecutionOption>
{
    public void Configure(EntityTypeBuilder<WorkflowExecutionOption> builder)
    {
        // builder.HasKey(
        //     executionOption => new
        //     {
        //         executionOption.AnalysisPromptCategoryId,
        //         executionOption.FeedbackExecutionWorkflowId
        //     }
        // );

        builder.HasOne(executionOptions => executionOptions.AnalysisPromptCategory)
            .WithMany(promptCategory => promptCategory.FeedbackWorkflowExecutionOptions)
            .HasForeignKey(pc => pc.AnalysisPromptCategoryId);

        builder.HasOne<WorkflowExecutionOption>()
            .WithMany(executionOptions => executionOptions.ChildExecutionOptions)
            .HasForeignKey(executionOptions => executionOptions.ParentId)
            //     executionOption => new
            //     { 
            //         executionOption.AnalysisPromptCategoryId,
            //         executionOption.FeedbackExecutionWorkflowId
            //     }
            // )
            .IsRequired(false);
    }
}