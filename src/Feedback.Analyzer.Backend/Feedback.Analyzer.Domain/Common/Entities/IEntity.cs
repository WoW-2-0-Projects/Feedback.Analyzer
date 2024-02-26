﻿namespace Feedback.Analyzer.Domain.Common.Entities;

/// <summary>
/// Defines an entity within the system.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the entity. This identifier should be globally unique within the system.
    /// </summary>
    public Guid Id { get; set; }
}
