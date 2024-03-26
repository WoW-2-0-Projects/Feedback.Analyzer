using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class FeedbackAnalysisResultConfiguration : IEntityTypeConfiguration<FeedbackAnalysisResult>
{
    public void Configure(EntityTypeBuilder<FeedbackAnalysisResult> builder)
    {
        builder.OwnsOne<FeedbackRelevance>(feedback => feedback.FeedbackRelevance);
        builder.OwnsOne<FeedbackOpinion>(feedback => feedback.FeedbackOpinion);
        builder.OwnsOne<FeedbackEntities>(feedback => feedback.FeedbackEntities);
        builder.OwnsOne<FeedbackMetrics>(feedback => feedback.FeedbackMetrics);
        builder.OwnsOne<AnalysisResult>(feedback => feedback.AnalysisResult)
            .HasOne<PromptExecutionHistory>(result => result.History)
            .WithOne()
            .HasForeignKey<AnalysisResult>(result => result.HistoryId);

        builder
            .HasOne<CustomerFeedback>(result => result.CustomerFeedback)
            .WithMany(feedback => feedback.FeedbackAnalysisResults)
            .HasForeignKey(result => result.CustomerFeedbackId);
        
        builder
            .HasOne<FeedbackAnalysisWorkflowResult>()
            .WithMany(workflowResult => workflowResult.FeedbackAnalysisResults)
            .HasForeignKey(feedbackResult => feedbackResult.FeedbackAnalysisWorkflowResultId);
    }
}