namespace Feedback.Analyzer.Api.Configurations;

public static partial class HostConfiguration
{
    /// <summary>
    /// Configures application builder
    /// </summary>
    /// <param name="builder">Application builder</param>
    /// <returns>Application builder</returns>
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddValidators()
            .AddMappers()
            .AddPersistence()
            .AddClientInfrastructure()
            .AddNotificationInfrastructure()
            .AddMediator()
            .AddDevTools()
            .AddExposers();
            
        
        return new(builder);
    }

    /// <summary>
    /// Configures application
    /// </summary>
    /// <param name="app">Application host</param>
    /// <returns>Application host</returns>
    public static async  ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        await app.MigrateDataBaseSchemasAsync();
        await app.SeedDataAsync();
        app
            .UseDevTools()
            .UseExposers();
        
        return app;
    }
}