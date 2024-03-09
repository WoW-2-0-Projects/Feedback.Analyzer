using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class FeedbackAnalysisWorkflowConfiguration : IEntityTypeConfiguration<FeedbackAnalysisWorkflow>
{
    public void Configure(EntityTypeBuilder<FeedbackAnalysisWorkflow> builder)
    {
        builder
            .HasOne(workflow => workflow.Product)
            .WithMany()
            .HasForeignKey(workflow => workflow.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(workflow => workflow.Workflow)
            .WithOne()
            .HasForeignKey<FeedbackAnalysisWorkflow>(workflow => workflow.Id)
            .OnDelete(DeleteBehavior.Restrict);

        // TODO : Create index between product Id and name
    }
}