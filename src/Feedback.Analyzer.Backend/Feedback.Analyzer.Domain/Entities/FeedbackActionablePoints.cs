namespace Feedback.Analyzer.Domain.Entities;

public class FeedbackActionablePoints
{
    public string[] GenericPoints { get; set; }

    public string[] SpecificPoints { get; set; }

    public string[] ActionablePoints { get; set; }

    public string[] NonActionablePoints { get; set; }
}