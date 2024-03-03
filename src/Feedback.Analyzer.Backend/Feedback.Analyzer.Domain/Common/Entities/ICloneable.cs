namespace Feedback.Analyzer.Domain.Common.Entities;

public interface ICloneable<out TClone>
{
    TClone Clone();
}