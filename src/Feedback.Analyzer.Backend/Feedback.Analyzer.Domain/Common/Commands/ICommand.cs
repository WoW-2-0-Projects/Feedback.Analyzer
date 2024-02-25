using MediatR;

namespace Feedback.Analyzer.Domain.Common.Commands;

/// <summary>
/// Represents a command in a CQRS (Command Query Responsibility Segregation) architecture.
/// </summary>
/// <typeparam name="TResult">The type of result produced by executing the command.</typeparam>
public interface ICommand<out TResult> : ICommand, IRequest<TResult>
{
    
}

/// <summary>
/// Marker interface for commands.
/// </summary>
public interface ICommand
{
    
}
