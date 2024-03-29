using Feedback.Analyzer.Application.Common.Prompts.Brokers;
using Feedback.Analyzer.Domain.Common.Exceptions;
using Feedback.Analyzer.Domain.Extensions;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

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

        var openAiExecutionSetting = new OpenAIPromptExecutionSettings
        {
            MaxTokens = 2048,
            Temperature = 0
        };

        var function = kernel.CreateFunctionFromPrompt(new PromptTemplateConfig
        {
            Template = prompt,
            ExecutionSettings =
            {
                {
                    "default",
                    openAiExecutionSetting
                }
            }
        });

        var executePromptAction = async () => await kernel.InvokeAsync<string>(function, kernelArguments, cancellationToken: cancellationToken);
        
        return executePromptAction.GetValueAsync();
    }
}