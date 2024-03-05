using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

/// <summary>
/// Configuration for the entity type <see cref="PromptExecutionHistory"/>.
/// </summary>
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