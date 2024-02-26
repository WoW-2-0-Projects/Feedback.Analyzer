namespace Feedback.Analyzer.Domain.Common.Commands;

/// <summary>
/// Represents options related to the execution of a command, particularly controlling data persistence.
/// </summary>
public struct CommandOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether changes made by the command should be automatically saved to the underlying data store.
    /// </summary>
    public bool SaveChanges { get; set; }

    /// <summary>
    /// Provides a default set of CommandOptions, likely with 'SaveChanges' set to true.
    /// </summary>
    public static CommandOptions Default => new(); 
}