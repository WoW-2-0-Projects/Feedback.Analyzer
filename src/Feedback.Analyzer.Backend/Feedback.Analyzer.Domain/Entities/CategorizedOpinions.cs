namespace Feedback.Analyzer.Domain.Entities;

public class CategorizedOpinions
{
    public string Category { get; set; } = default!;

    public string[] Opinions { get; set; } = default!;
}