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
            .AddSerializers()
            .AddMappers()
            .AddValidators()
            .AddCaching()
            .AddPersistence()
            .AddSemanticKernelInfrastructure()
            .AddMediator()
            .AddEventBus()
            .AddIdentityInfrastructure()
            .AddClientInfrastructure()
            .AddFeedbackInfrastructure()
            .AddSemanticAnalysisInfrastructure()
            .AddRequestContextTools()
            .AddCors()
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
        app.UseCors();
        app
            .UseDevTools()
            .UseExposers();
        
        return app;
    }
}