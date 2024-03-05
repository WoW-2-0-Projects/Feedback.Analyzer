using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class AnalysisPromptCategoryConfiguration : IEntityTypeConfiguration<AnalysisPromptCategory>
{
    public void Configure(EntityTypeBuilder<AnalysisPromptCategory> builder)
    {
        builder
            .Property(category => category.Category)
            .HasConversion<string>()
            .IsRequired();

        builder
            .HasIndex(category => category.Category);
        
        builder
            .HasOne(category => category.SelectedPrompt)
            .WithOne()
            .HasForeignKey<AnalysisPromptCategory>(category => category.SelectedPromptId)
            .IsRequired(false);
    }
}