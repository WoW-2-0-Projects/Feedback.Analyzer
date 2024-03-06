namespace Feedback.Analyzer.Domain.Enums;

/// <summary>
/// Enumerates the different types of prompts that can be used in the feedback analyzer.
/// </summary>
public enum PromptType
{
    /// <summary>
    /// Indicates an analysis prompt, which is a question that requires a detailed response.
    /// </summary>
    AnalysisPrompt,

    /// <summary>
    /// Indicates an assertion prompt, which is a question that requires a simple "yes" or "no" response.
    /// </summary>
    AssertionPrompt
}