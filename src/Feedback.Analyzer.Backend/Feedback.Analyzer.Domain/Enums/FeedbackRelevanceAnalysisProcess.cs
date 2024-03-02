namespace Feedback.Prompt.Analyzer.Domain.Enums;

/// <summary>
/// Represents feedback relevance analysis process
/// </summary>
public enum FeedbackRelevanceAnalysisProcess
{
    /// <summary>
    /// Refers to the content safety analysis
    /// </summary>
    ContentSafetyAnalysis,
    
    /// <summary>
    /// Refers to the language recognition
    /// </summary>
    RecognizeLanguages,
    
    /// <summary>
    /// Refers to the relevance analysis
    /// </summary>
    RelevanceAnalysis,
    
    /// <summary>
    /// Refers to the extraction of relevant content
    /// </summary>
    ExtractRelevantContent,
    
    /// <summary>
    /// Refers to the redaction of personal information
    /// </summary>
    RedactPersonalInformation
}