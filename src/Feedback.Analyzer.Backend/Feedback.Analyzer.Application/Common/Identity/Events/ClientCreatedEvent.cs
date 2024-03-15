using Feedback.Analyzer.Domain.Common.Events;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.Identity.Events;

/// <summary>
/// This class represents an event that is triggered when a user is created.
/// </summary>
public record ClientCreatedEvent(Client createdClient) : Event
{
    /// <summary>
    /// Gets or sets the information about the user that was created.
    /// </summary>
    public Client CreatedClient { get; set; } = createdClient;
}