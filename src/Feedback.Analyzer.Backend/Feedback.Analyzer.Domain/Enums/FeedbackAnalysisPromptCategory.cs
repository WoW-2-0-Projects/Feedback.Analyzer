using System.ComponentModel;

namespace Feedback.Analyzer.Domain.Enums;

/// <summary>
/// Represents feedback relevance analysis process
/// </summary>
public enum FeedbackAnalysisPromptCategory
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
    /// Refers to the extraction of relevant content
    /// </summary>
    [Description("Identify Named Entities")] EntityIdentification,

    /// <summary>
    /// Refers to the redaction of personal information
    /// </summary>
    [Description("Redact Personal Information")] PersonalInformationRedaction,
    
    /// <summary>
    /// Refers to the extraction of opinion points
    /// </summary>
    [Description("Mine Overall Opinion")] OpinionMining,
    
    /// <summary>
    /// Refers to the extraction of opinion points
    /// </summary>
    [Description("Extract questions")] QuestionPointsExtraction,
    
    /// <summary>
    /// Refers to the extraction of opinion points
    /// </summary>
    [Description("Extract Opinion Points")] OpinionPointsExtraction,
    
    /// <summary>
    /// Refers to the extraction of opinion points
    /// </summary>
    [Description("Analyze Opinions for Action")] ActionableOpinionsAnalysis,
    
    /// <summary>
    /// Refers to the extraction of opinion points
    /// </summary>
    [Description("Analyze Questions for Action")] ActionableQuestionsAnalysis,
    
    /// <summary>
    /// Refers to the extraction of opinion points
    /// </summary>
    [Description("Categorize Opinions")] OpinionsCategoryAnalysis,
}