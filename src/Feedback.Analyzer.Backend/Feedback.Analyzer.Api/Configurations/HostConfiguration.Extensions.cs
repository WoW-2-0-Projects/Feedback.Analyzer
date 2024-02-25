namespace Feedback.Analyzer.Api.Configurations;

public static partial class HostConfiguration
{
    /// <summary>
    ///  Configures exposers including controllers and routing.
    /// </summary>
    /// <param name="builder">>Application builder</param>
    /// <returns>Application builder</returns>
    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();
        
        return builder;
    }
    
    /// <summary>
    /// Configures devTools including controllers
    /// </summary>
    /// <param name="builder"></param>
    /// <returns>Application builder</returns>
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        return builder;
    }
    
    /// <summary>
    /// Add Controller middleWhere
    /// </summary>
    /// <param name="app">Application host</param>
    /// <returns>Application host</returns>
    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();
        
        return app;
    }
    
    /// <summary>
    /// Add Controller middleWhere
    /// </summary>
    /// <param name="app">Application host</param>
    /// <returns>Application host</returns>
    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        
        return app;
    }
}
    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        // register ef interceptors
    /// <summary>
    /// Adds persistence-related services to the web application builder.
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> instance to which services are being added.</param>
    /// <returns>The updated <see cref="WebApplicationBuilder"/> instance.</returns>
        
        //register db context
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseInMemoryDatabase("FeedbackAnalyzer");
        });
        return builder;
    }
