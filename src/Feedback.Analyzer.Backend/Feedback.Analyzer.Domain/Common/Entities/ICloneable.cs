namespace Feedback.Analyzer.Domain.Common.Entities;

/// <summary>
/// Represents an interface for cloning objects of type TClone.
/// </summary>
/// <typeparam name="TClone">The type of object to clone.</typeparam>
public interface ICloneable<out TClone>
{
    /// <summary>
    /// Creates a clone of the object.
    /// </summary>
    TClone Clone();
}
