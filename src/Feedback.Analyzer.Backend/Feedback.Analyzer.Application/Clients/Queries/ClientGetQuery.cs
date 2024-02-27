using Feedback.Analyzer.Application.Clients.Model;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Clients.Queries;

/// <summary>
/// Represents a command to retrieve a collection of clients based on specified filtering criteria.
/// </summary>
public class ClientGetQuery : IQuery<ICollection<Client>>
{    
     public ClientFilter ClientFilter { get; set; } = default!;
}