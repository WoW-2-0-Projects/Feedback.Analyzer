using Feedback.Analyzer.Domain.Common.Prompts;
using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class AnalysisPromptCategoryConfiguration : IEntityTypeConfiguration<AnalysisPromptCategory>
{
    public void Configure(EntityTypeBuilder<AnalysisPromptCategory> builder)
    {
        builder
            .Property(category => category.Type)
            .HasConversion<string>()
            .IsRequired();

        builder
            .HasIndex(category => category.Type);
        
        builder
            .HasOne<AnalysisPrompt>()
            .WithOne()
            .HasForeignKey<AnalysisPromptCategory>(category => category.SelectedPromptId)
            .IsRequired(false);
    }
}