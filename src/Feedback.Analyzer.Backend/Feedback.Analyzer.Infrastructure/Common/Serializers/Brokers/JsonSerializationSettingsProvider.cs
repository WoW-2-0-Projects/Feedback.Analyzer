using Feedback.Analyzer.Application.Common.Serializers.Brokers;
using Force.DeepCloner;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Feedback.Analyzer.Infrastructure.Common.Serializers.Brokers;

/// <summary>
/// Implementation of the IJsonSerializationSettingsProvider interface.
/// Provides JSON serialization settings with default configurations.
/// </summary>
public class JsonSerializationSettingsProvider : IJsonSerializationSettingsProvider
{
    private readonly JsonSerializerSettings _defaultSettings; 
    private readonly JsonSerializerSettings _eventBusSettings; 

    public JsonSerializationSettingsProvider()
    {
        _defaultSettings = Configure(new JsonSerializerSettings());
        _eventBusSettings = ConfigureForEventBus(new JsonSerializerSettings());
    }
    
    public JsonSerializerSettings Configure(JsonSerializerSettings settings)
    {
        // Configures the output JSON formatting for readability
        settings.Formatting = Formatting.Indented;
        
        // Configures reference loops when serializing objects with circular references
        settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        
        // Configures the contract resolver to use camelCase for property names
        settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        
        //Ignores null values during serialization
        settings.NullValueHandling = NullValueHandling.Ignore;

        return settings;
    }

    public JsonSerializerSettings ConfigureForEventBus(JsonSerializerSettings settings)
    {
        // Configure(settings);
        
        // Configures the contract resolver to use default property names
        // settings.ContractResolver = new DefaultContractResolver();
        
        // Configures the type name handling for serialization
        // settings.TypeNameHandling = TypeNameHandling.All;
        
        settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        
        return settings;
    }
    
    public JsonSerializerSettings Get(bool clone = false) => clone ? _defaultSettings.DeepClone() : _defaultSettings;
    
    public JsonSerializerSettings GetForEventBus(bool clone = false) => clone ? _eventBusSettings.DeepClone() : _eventBusSettings;
}