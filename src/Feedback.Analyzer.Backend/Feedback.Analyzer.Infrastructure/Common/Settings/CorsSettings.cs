namespace Feedback.Analyzer.Infrastructure.Common.Settings;

/// <summary>
/// Represents CORS settings
/// </summary>
public record CorsSettings
{
    /// <summary>
    /// Gets collection of allowed origins
    /// </summary>
    public string[] AllowedOrigins { get; init; } = default!;
    
    /// <summary>
    /// Gets value indicating whether any header is allowed
    /// </summary>
    public bool AllowAnyHeaders { get; init; }
    
    /// <summary>
    /// Gets value indicating whether any method is allowed
    /// </summary>
    public bool AllowAnyMethods { get; init; }
    
    /// <summary>
    /// Gets value indicating whether credentials are allowed
    /// </summary>
    public bool AllowCredentials { get; init; }
}