using Newtonsoft.Json;

namespace Feedback.Analyzer.Application.Common.Serializers.Brokers;

/// <summary>
/// Defines JSON serialization settings provider for customizing serialization
/// </summary>
public interface IJsonSerializationSettingsProvider
{
    /// <summary>
    /// Configures existing serialization settings for general use
    /// </summary>
    /// <param name="settings">Instance of <see cref="JsonSerializerSettings"/> to configure</param>
    /// <returns>Same instance of <see cref="JsonSerializerSettings"/> after default configuration</returns>
    JsonSerializerSettings Configure(JsonSerializerSettings settings);

    /// <summary>
    /// Configures existing serialization settings for serialization in event bus
    /// </summary>
    /// <param name="settings">Instance of <see cref="JsonSerializerSettings"/> to configure</param>
    /// <returns>Same instance of <see cref="JsonSerializerSettings"/> after configuration for event bus</returns>
    JsonSerializerSettings ConfigureForEventBus(JsonSerializerSettings settings);

    /// <summary>
    /// Gets serialization settings for general use
    /// </summary>
    /// <param name="clone">Indicates whether to return a cloned instance of the settings</param>
    /// <returns>Instance of <see cref="JsonSerializerSettings"/> with default configurations</returns>
    JsonSerializerSettings Get(bool clone = false);

    /// <summary>
    /// Gets serialization settings for event bus
    /// </summary>
    /// <param name="clone">Indicates whether to return a cloned instance of the settings</param>
    /// <returns>Instance of <see cref="JsonSerializerSettings"/> with event bus serialization configurations</returns>
    JsonSerializerSettings GetForEventBus(bool clone = false);
}