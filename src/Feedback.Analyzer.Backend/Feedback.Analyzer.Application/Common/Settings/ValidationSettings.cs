namespace Feedback.Analyzer.Application.Common.Settings;

/// <summary>
/// Represents the settings for validation.
/// </summary>
public record ValidationSettings
{
    /// <summary>
    /// Gets or sets the regular expression pattern for validating names.
    /// </summary>
    public string NameRegexPattern { get; init; } = default!;
    
    /// <summary>
    /// Gets or sets the regular expression pattern for validating descriptions.
    /// </summary>
    public string EmailRegexPattern { get; init; } = default!;
}