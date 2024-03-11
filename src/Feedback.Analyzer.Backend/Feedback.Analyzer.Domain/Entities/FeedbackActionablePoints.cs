namespace Feedback.Analyzer.Domain.Entities;

public class FeedbackActionablePoints
{
    public string[] GenericPoints { get; set; } = Array.Empty<string>();

    public string[] SpecificPoints { get; set; } = Array.Empty<string>();

    public string[] ActionablePoints { get; set; } = Array.Empty<string>();

    public string[] NonActionablePoints { get; set; } = Array.Empty<string>();
}