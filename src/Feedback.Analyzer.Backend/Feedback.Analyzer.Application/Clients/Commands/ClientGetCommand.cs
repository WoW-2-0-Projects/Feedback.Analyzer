using Feedback.Analyzer.Application.Clients.Model;
using Feedback.Analyzer.Domain.Common.Commands;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Clients.Commands;

public class ClientGetCommand : ICommand<IQueryable<Client>>
{
    public ClientFilter? ClientFilter { get; set; }
}