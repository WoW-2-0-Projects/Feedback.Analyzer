using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class PromptExecutionHistoryConfiguration : IEntityTypeConfiguration<PromptExecutionHistory>
{
    public void Configure(EntityTypeBuilder<PromptExecutionHistory> builder)
    {
        builder
            .HasOne(history => history.Prompt)
            .WithMany(prompt => prompt.ExecutionHistories)
            .HasForeignKey(entity => entity.PromptId);
    }
}