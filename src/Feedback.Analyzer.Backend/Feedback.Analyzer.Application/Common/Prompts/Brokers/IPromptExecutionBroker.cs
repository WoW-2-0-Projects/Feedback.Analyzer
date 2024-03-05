using Feedback.Analyzer.Domain.Common.Exceptions;
using Microsoft.SemanticKernel;

namespace Feedback.Analyzer.Application.Common.Prompts.Brokers;

public interface IPromptExecutionBroker
{
    ValueTask<FuncResult<string?>> ExecutePromptAsync(string prompt, KernelArguments arguments, CancellationToken cancellationToken = default);
}