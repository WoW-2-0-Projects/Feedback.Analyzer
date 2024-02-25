namespace Feedback.Analyzer.Api.Configurations;

public static partial class HostConfigurations
{
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddPersistence();
        
        return new(builder);
    }
    
    public static async ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        return app;
    }
}