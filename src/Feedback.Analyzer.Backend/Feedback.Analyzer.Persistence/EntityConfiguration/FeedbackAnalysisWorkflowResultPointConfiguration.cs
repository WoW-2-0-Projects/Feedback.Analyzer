// using Feedback.Analyzer.Domain.Entities;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace Feedback.Analyzer.Persistence.EntityConfiguration;
//
// public class FeedbackAnalysisWorkflowResultPointConfiguration : IEntityTypeConfiguration<FeedbackAnalysisWorkflowResultPoint>
// {
//     public void Configure(EntityTypeBuilder<FeedbackAnalysisWorkflowResultPoint> builder)
//     {
//         builder
//             .HasKey(
//             point => new
//             {
//                 point.WorkflowResultId,
//                 point.FeedbackResultId
//             }
//         );
//         
//         builder
//             .Property(point => point.Point)
//             .HasMaxLength(128)
//             .IsRequired();
//
//         builder
//             .HasOne<FeedbackAnalysisWorkflowResult>()
//             .WithMany(workflowResult => workflowResult.KeyPoints)
//             .HasForeignKey(stats => stats.WorkflowResultId);
//         
//         builder
//             .HasOne<FeedbackAnalysisResult>()
//             .WithMany()
//             .HasForeignKey(stats => stats.FeedbackResultId);
//     }
// }