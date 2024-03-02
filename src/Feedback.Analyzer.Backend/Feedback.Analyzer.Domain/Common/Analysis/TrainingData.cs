namespace Feedback.Prompt.Analyzer.Domain.Common.Analysis;

/// <summary>
/// Represents generic training data
/// </summary>
public class TrainingData<TTarget, TData, TExpectedResult>
{ 
    /// <summary>
    /// Gets or sets training feedback target
    /// </summary>
    public TTarget Target { get; set; } = default!;

    /// <summary>
    /// Gets or sets feedback data
    /// </summary>
    public TData Data { get; set; } = default!;

    /// <summary>
    /// Gets or sets feedback prompt analyzing expected result
    /// </summary>
    public TExpectedResult ExpectedResult { get; set; } = default!;  
}