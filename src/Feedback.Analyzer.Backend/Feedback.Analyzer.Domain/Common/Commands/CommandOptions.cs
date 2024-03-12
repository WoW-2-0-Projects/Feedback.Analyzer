namespace Feedback.Analyzer.Domain.Common.Commands;

/// <summary>
/// Represents options related to the execution of a command, particularly controlling data persistence.
/// </summary>
public struct CommandOptions()
{
    /// <summary>
    /// Gets or sets a value indicating whether changes made by the command should be automatically saved to the underlying data store.
    /// </summary>
    public bool SkipSaveChanges { get; set; } = false;
}