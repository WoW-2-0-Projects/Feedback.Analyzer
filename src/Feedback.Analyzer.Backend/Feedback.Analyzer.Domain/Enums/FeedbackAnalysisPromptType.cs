using System.ComponentModel;

namespace Feedback.Analyzer.Domain.Enums;

/// <summary>
/// Represents feedback relevance analysis process
/// </summary>
public enum FeedbackAnalysisPromptType
{
    /// <summary>
    /// Refers to the content safety analysis
    /// </summary>
    [Description("Content Safety Analysis")] ContentSafetyAnalysis,

    /// <summary>
    /// Refers to the language recognition
    /// </summary>
    [Description("Language Recognition")] LanguageRecognition,

    /// <summary>
    /// Refers to the relevance analysis
    /// </summary>
    [Description("Relevance Analysis")] RelevanceAnalysis,

    /// <summary>
    /// Refers to the extraction of relevant content
    /// </summary>
    [Description("Extract Relevant Content")] RelevantContentExtraction,

    /// <summary>
    /// Refers to the redaction of personal information
    /// </summary>
    [Description("Redact Personal Information")] PersonalInformationRedaction,
    
    /// <summary>
    /// Refers to the extraction of opinion points
    /// </summary>
    [Description("Redact Personal Information")] OpinionPointsExtraction
}