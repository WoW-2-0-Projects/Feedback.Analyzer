using Feedback.Analyzer.Application.Clients.Models;
using Feedback.Analyzer.Domain.Common.Queries;

namespace Feedback.Analyzer.Application.Clients.Queries;

/// <summary>
/// Represents a command to retrieve a collection of clients based on specified filtering criteria.
/// </summary>
public record ClientGetQuery : IQuery<ICollection<ClientDto>>
{    
     public ClientFilter ClientFilter { get; init; } = default!;
}