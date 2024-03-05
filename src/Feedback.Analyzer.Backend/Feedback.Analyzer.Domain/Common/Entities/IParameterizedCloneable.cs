namespace Feedback.Analyzer.Domain.Common.Entities;

/// <summary>
/// Represents an interface for cloning objects of type TClone.
/// </summary>
/// <typeparam name="TClone">The type of object to clone.</typeparam>
/// <typeparam name="TArgs">Type of arguments to pass</typeparam>
public interface IParameterizedCloneable<in TArgs, out TClone>
{
    /// <summary>
    /// Creates a clone of the object.
    /// </summary>
    TClone Clone(TArgs args);
}