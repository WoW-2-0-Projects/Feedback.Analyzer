using Feedback.Analyzer.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Api.Configurations;

public static partial class HostConfigurations
{
    /// <summary>
    /// Adds persistence-related services to the web application builder.
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> instance to which services are being added.</param>
    /// <returns>The updated <see cref="WebApplicationBuilder"/> instance.</returns>
    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        // register ef interceptors
        
        //register db context
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseInMemoryDatabase("FeedbackAnalyzer");
        });

        return builder;
    }
}