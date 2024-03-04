﻿using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class AnalysisPromptConfiguration : IEntityTypeConfiguration<AnalysisPrompt>
{
    public void Configure(EntityTypeBuilder<AnalysisPrompt> builder)
    {
        builder.Property(analysisPrompt => analysisPrompt.Prompt).HasMaxLength(32768).IsRequired();
        
        builder
            .HasOne(prompt => prompt.Category)
            .WithMany(category => category.Prompts)
            .HasForeignKey(prompt => prompt.CategoryId);
        
    }
}