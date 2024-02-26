namespace Feedback.Analyzer.Domain.Common.Queries;

/// <summary>
/// Represents a set of options to configure data querying behavior.
/// </summary>
public struct QueryOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether change tracking should be disabled for query results. 
    /// Disabling change tracking can potentially improve performance.
    /// </summary>
    public bool AsNoTracking { get; set; }
}