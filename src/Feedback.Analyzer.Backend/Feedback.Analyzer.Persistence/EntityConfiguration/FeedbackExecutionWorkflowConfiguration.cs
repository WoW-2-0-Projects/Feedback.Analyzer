using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class FeedbackExecutionWorkflowConfiguration : IEntityTypeConfiguration<FeedbackExecutionWorkflow>
{
    public void Configure(EntityTypeBuilder<FeedbackExecutionWorkflow> builder)
    {
        builder
            .HasMany<AnalysisPromptCategory>(workflow => workflow.AnalysisPromptCategories)
            .WithMany(analysis => analysis.FeedbackExecutionWorkflows)
            .UsingEntity<WorkflowPromptCategoryExecutionOptions>(self =>
            {
                self.HasKey(relation => new
                    { relation.FeedbackExecutionWorkflowId, relation.AnalysisPromptCategoryId });
                self.ToTable($"{nameof(WorkflowPromptCategoryExecutionOptions)}");
            });
    }
}