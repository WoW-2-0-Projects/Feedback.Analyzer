namespace Feedback.Analyzer.Domain.Entities;

public class EntityIdentification
{
    public Guid FeedbackId { get; set; }

    public string Key { get; set; }

    public string Value { get; set; }
}