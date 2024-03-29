using System.ComponentModel;

namespace Feedback.Analyzer.Domain.Common.Charts;

/// <summary>
/// Defines chart data type and color
/// </summary>
public enum ChartDataTypeColor : byte
{
    /// <summary>
    /// Represents positive points
    /// </summary>
    [Description("#4CAF50")] PositivePoints = 0,

    /// <summary>
    /// Represents negative points
    /// </summary>
    [Description("#F44336")] NegativePoints = 1,

    /// <summary>
    /// Represents neutral points
    /// </summary>
    [Description("#9E9E9E")] NeutralPoints = 2,

    /// <summary>
    /// Represents questions
    /// </summary>
    [Description("#2196F3")] Questions = 3,

    /// <summary>
    /// Represents ideas
    /// </summary>
    [Description("#FFC107")] Ideas = 4,

    /// <summary>
    /// Represents impact score
    /// </summary>
    [Description("#673AB7")] ImpactScore = 5
}