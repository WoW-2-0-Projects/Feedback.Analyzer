namespace Feedback.Analyzer.Application.Common.Settings;

public class ValidationSettings
{
    /// <summary>
    /// Gets or sets the regular expression pattern for validating names.
    /// </summary>
    public string NameRegexPattern { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the regular expression pattern for validating descriptions.
    /// </summary>
    public string EmailRegexPattern { get; set; } = default!;
}