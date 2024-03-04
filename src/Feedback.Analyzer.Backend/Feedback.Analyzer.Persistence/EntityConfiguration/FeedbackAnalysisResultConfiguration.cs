using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class FeedbackAnalysisResultConfiguration : IEntityTypeConfiguration<FeedbackAnalysisResult>
{
    public void Configure(EntityTypeBuilder<FeedbackAnalysisResult> builder)
    {
        builder.HasKey(feedback => feedback.Id);
        
        builder.OwnsOne<FeedbackRelevance>(feedback => feedback.FeedbackRelevance);
        builder.OwnsOne<FeedbackOpinion>(feedback => feedback.FeedbackOpinion);
        builder.OwnsOne<FeedbackActionablePoints>(feedback => feedback.FeedbackActionablePoints);
        builder.OwnsOne<FeedbackEntities>(feedback => feedback.FeedbackEntities);
        builder.OwnsOne<FeedbackMetrics>(feedback => feedback.FeedbackMetrics);

        builder.OwnsMany(feedback => feedback.CategorizedOpinions);
        builder.OwnsMany(feedback => feedback.EntityIdentifications);

        builder
            .HasOne<CustomerFeedback>(feedback => feedback.CustomerFeedback)
            .WithOne(customerFeedback => customerFeedback.FeedbackAnalysisResult)
            .HasForeignKey<FeedbackAnalysisResult>(feedback => feedback.CustomerFeedbackId);
    }
}