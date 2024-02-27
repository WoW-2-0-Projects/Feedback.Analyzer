namespace Feedback.Analyzer.Infrastructure.Common.Settings;

/// <summary>
/// Represents data access settings
/// </summary>
public record DataAccessSettings
{
    /// <summary>
    /// Determines whether to use in memory database
    /// </summary>
    public bool UseInMemoryDatabase { get; init; }
}