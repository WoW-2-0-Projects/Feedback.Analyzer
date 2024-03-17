using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class FeedbackAnalysisWorkflowResultConfiguration : IEntityTypeConfiguration<FeedbackAnalysisWorkflowResult>
{
    public void Configure(EntityTypeBuilder<FeedbackAnalysisWorkflowResult> builder)
    {
        
        builder
            .HasOne<FeedbackAnalysisWorkflow>(result => result.Workflow)
            .WithMany(workflow => workflow.Results)
            .HasForeignKey(result => result.WorkflowId);
        
    }
}