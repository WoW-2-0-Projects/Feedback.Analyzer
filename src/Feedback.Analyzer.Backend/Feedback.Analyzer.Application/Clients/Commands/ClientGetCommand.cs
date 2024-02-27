using Feedback.Analyzer.Application.Clients.Model;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Clients.Commands;

/// <summary>
/// Represents a command to retrieve a collection of clients based on specified filtering criteria.
/// </summary>
public class ClientGetCommand : ICommand<IQueryable<Client>>
{
    public ClientFilter? ClientFilter { get; set; }
}