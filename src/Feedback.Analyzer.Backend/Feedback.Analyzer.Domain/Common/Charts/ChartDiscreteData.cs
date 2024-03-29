namespace Feedback.Analyzer.Domain.Common.Charts;

/// <summary>
/// Represents chart data with elements count
/// </summary>
public class ChartDiscreteData : ChartData
{
    /// <summary>
    /// Gets or sets elements count
    /// </summary>
    public uint Count { get; set; }
}