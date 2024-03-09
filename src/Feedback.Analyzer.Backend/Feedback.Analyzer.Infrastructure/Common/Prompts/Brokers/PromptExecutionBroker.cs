using Feedback.Analyzer.Application.Common.Prompts.Brokers;
using Feedback.Analyzer.Domain.Common.Exceptions;
using Feedback.Analyzer.Domain.Extensions;
using Microsoft.SemanticKernel;

namespace Feedback.Analyzer.Infrastructure.Common.Prompts.Brokers;

public class PromptExecutionBroker(Kernel kernel) : IPromptExecutionBroker
{
    public ValueTask<FuncResult<string?>> ExecutePromptAsync(string prompt, KernelArguments arguments, CancellationToken cancellationToken = default)
    {
        var executePromptAction = async () => await kernel.InvokePromptAsync<string>(prompt, arguments, cancellationToken: cancellationToken);
        return executePromptAction.GetValueAsync();
    }
}