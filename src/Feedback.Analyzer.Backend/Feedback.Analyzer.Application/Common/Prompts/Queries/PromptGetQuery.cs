﻿using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Application.Organizations.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Common.Prompts.Queries;

/// <summary>
///  Represents a query to retrieve a collection of organizations.
/// </summary>
public record PromptGetQuery : IQuery<ICollection<AnalysisPromptDto>>
{
    /// <summary>
    ///  Gets or sets the filter to apply when retrieving organizations.
    /// </summary>
    public PromptFilter Filter { get; set; } = default!;
}