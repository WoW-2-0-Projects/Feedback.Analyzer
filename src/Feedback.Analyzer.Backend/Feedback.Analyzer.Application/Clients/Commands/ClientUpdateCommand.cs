using Feedback.Analyzer.Api.Models.DTOs;
using Feedback.Analyzer.Domain.Common.Commands;

namespace Feedback.Analyzer.Application.Clients.Commands;

/// <summary>
/// Represents a command to update a client entity.
/// </summary>
public record ClientUpdateCommand : ICommand<ClientDto>
{
    public ClientDto Client { get; init; } = default!;
}