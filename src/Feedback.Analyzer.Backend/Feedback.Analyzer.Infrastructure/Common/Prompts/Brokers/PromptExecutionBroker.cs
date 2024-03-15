using Feedback.Analyzer.Application.Common.Prompts.Brokers;
using Feedback.Analyzer.Domain.Common.Exceptions;
using Feedback.Analyzer.Domain.Extensions;
using Microsoft.SemanticKernel;

namespace Feedback.Analyzer.Infrastructure.Common.Prompts.Brokers;

/// <summary>
/// Provides prompt execution broker functionality and mapping arguments and results
/// </summary>
/// <param name="kernel"></param>
public class PromptExecutionBroker(Kernel kernel) : IPromptExecutionBroker
{
    public ValueTask<FuncResult<string?>> ExecutePromptAsync(string prompt, Dictionary<string,string> arguments, CancellationToken cancellationToken = default)
    {
        // Query prompt and feedback
        var kernelArguments = new KernelArguments();
        foreach (var keyValuePair in arguments)
            kernelArguments.Add(keyValuePair.Key, keyValuePair.Value);
        
        var executePromptAction = async () => await kernel.InvokePromptAsync<string>(prompt, kernelArguments, cancellationToken: cancellationToken);
        return executePromptAction.GetValueAsync();
    }
}