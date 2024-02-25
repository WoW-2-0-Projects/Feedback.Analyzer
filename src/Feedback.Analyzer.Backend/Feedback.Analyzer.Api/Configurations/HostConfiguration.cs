namespace Feedback.Analyzer.Api.Configurations;

public static partial class HostConfiguration
{
    /// <summary>
    /// Configures the web application asynchronously.
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> instance to configure.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation, containing the updated <see cref="WebApplicationBuilder"/> instance.</returns>
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddPersistence();
        
        return new(builder);
    }
    
    /// <summary>
    /// Configures the web application asynchronously.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance to configure.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation, containing the same <see cref="WebApplication"/> instance.</returns>
    public static async ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        return app;
    }
}