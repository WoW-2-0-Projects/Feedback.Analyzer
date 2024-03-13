using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class WorkflowExecutionOptionsConfiguration : IEntityTypeConfiguration<WorkflowExecutionOption>
{
    public void Configure(EntityTypeBuilder<WorkflowExecutionOption> builder)
    {
        builder.HasOne(options => options.Workflow)
               .WithMany(workflow => workflow.WorkflowExecutionOptions)
               .HasForeignKey(options => options.AnalysisWorkflowId);
        
        builder.HasOne(options => options.AnalysisPromptCategory)
               .WithMany(promptCategory => promptCategory.WorkflowExecutionOptions)
               .HasForeignKey(options => options.AnalysisPromptCategoryId);
        
        builder.HasOne<WorkflowExecutionOption>()
               .WithMany(executionOptions => executionOptions.ChildExecutionOptions)
               .HasForeignKey(executionOptions => executionOptions.ParentId).IsRequired(false);
    }
}