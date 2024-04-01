using System.ComponentModel;

namespace Feedback.Analyzer.Domain.Common.Charts;

/// <summary>
/// Defines chart data type and color
/// </summary>
public enum AnalysisResultType : byte
{
    /// <summary>
    /// Represents positive opinion points
    /// </summary>
    [Description("#4CAF50")] PositivePoint = 0,

    /// <summary>
    /// Represents negative opinion points
    /// </summary>
    [Description("#F44336")] NegativePoint = 1,

    /// <summary>
    /// Represents overall positive opinion
    /// </summary>
    [Description("#4CAF50")] PositiveOpinion = 2,

    /// <summary>
    /// Represents overall neutral points
    /// </summary>
    [Description("#9E9E9E")] NeutralPoint = 3,

    /// <summary>
    /// Represents overall negative opinion
    /// </summary>
    [Description("#F44336")] NegativeOpinion = 4,

    /// <summary>
    /// Represents questions
    /// </summary>
    [Description("#2196F3")] Question = 5,

    /// <summary>
    /// Represents ideas
    /// </summary>
    [Description("#FFC107")] Idea = 6,

    /// <summary>
    /// Represents impact score
    /// </summary>
    [Description("#673AB7")] ImpactScore = 7
}