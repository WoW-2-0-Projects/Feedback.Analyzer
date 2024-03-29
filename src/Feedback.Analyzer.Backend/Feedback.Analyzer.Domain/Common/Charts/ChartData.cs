using System.ComponentModel.DataAnnotations.Schema;

namespace Feedback.Analyzer.Domain.Common.Charts;

/// <summary>
/// Represents chart data
/// </summary>
public class ChartData
{
    /// <summary>
    /// Gets or sets chart label
    /// </summary>
    public string Label { get; init; } = default!;

    /// <summary>
    /// Gets or sets chart section value
    /// </summary>
    public float Percentage { get; init; }
}