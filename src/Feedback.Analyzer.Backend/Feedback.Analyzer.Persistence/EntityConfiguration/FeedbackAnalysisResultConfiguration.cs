using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class FeedbackAnalysisResultConfiguration : IEntityTypeConfiguration<FeedbackAnalysisResult>
{
    public void Configure(EntityTypeBuilder<FeedbackAnalysisResult> builder)
    {
        builder.OwnsOne<FeedbackRelevance>(feedback => feedback.FeedbackRelevance);
        builder.OwnsOne<FeedbackOpinion>(feedback => feedback.FeedbackOpinion);
        builder.OwnsOne<FeedbackActionablePoints>(feedback => feedback.FeedbackActionablePoints);
        builder.OwnsOne<FeedbackEntities>(feedback => feedback.FeedbackEntities);
        builder.OwnsOne<FeedbackMetrics>(feedback => feedback.FeedbackMetrics);

        builder
            .Property(feedback => feedback.CategorizedOpinions)
            .HasColumnType("json")
            .HasConversion(
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<List<CategorizedOpinions>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })!);
            
        builder
            .Property(feedback => feedback.EntityIdentifications)
            .HasColumnType("json")
            .HasConversion(
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<List<EntityIdentification>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })!);

        builder
            .HasOne<CustomerFeedback>(feedback => feedback.CustomerFeedback)
            .WithOne(customerFeedback => customerFeedback.FeedbackAnalysisResult)
            .HasForeignKey<FeedbackAnalysisResult>(feedback => feedback.CustomerFeedbackId);
        
        builder
            .HasOne<FeedbackAnalysisWorkflowResult>()
            .WithMany(workflowResult => workflowResult.FeedbackAnalysisResults)
            .HasForeignKey(feedbackResult => feedbackResult.FeedbackAnalysisWorkflowResultId);
    }
}