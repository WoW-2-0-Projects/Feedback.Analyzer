using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class AnalysisPromptCategoryConfiguration : IEntityTypeConfiguration<AnalysisPromptCategory>
{
    public void Configure(EntityTypeBuilder<AnalysisPromptCategory> builder)
    {
        builder.Property(category => category.Category)
            .HasConversion<string>()
            .HasMaxLength(256)
            .IsRequired();

        builder.HasIndex(category => category.Category);

        builder.HasOne(category => category.SelectedPrompt)
            .WithOne()
            .HasForeignKey<AnalysisPromptCategory>(category => category.SelectedPromptId)
            .IsRequired(false);
    }
}