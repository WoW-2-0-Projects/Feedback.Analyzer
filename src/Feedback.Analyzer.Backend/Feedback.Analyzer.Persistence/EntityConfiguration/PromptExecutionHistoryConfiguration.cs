using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class PromptExecutionHistoryConfiguration : IEntityTypeConfiguration<PromptExecutionHistory>
{
    public void Configure(EntityTypeBuilder<PromptExecutionHistory> builder)
    {
        builder.HasOne<AnalysisPrompt>().WithMany().HasForeignKey(entity => entity.PromptId);
    }
}