using System.Reflection;
using Feedback.Analyzer.Api.Data;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Application.Common.Caching;
using Feedback.Analyzer.Application.Common.EventBus.Brokers;
using Feedback.Analyzer.Application.Common.Identity.Services;
using Feedback.Analyzer.Application.Common.Settings;
using Feedback.Analyzer.Application.Serializers;
using Feedback.Analyzer.Domain.Brokers;
using Feedback.Analyzer.Infrastructure.Clients.Services;
using Feedback.Analyzer.Infrastructure.Common.Settings;
using Feedback.Analyzer.Infrastructure.Serializers;
using Feedback.Analyzer.Persistence.Caching.Brokers;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Api.Configurations;

public static partial class HostConfiguration
{
    private static readonly ICollection<Assembly> Assemblies;

    static HostConfiguration()
    {
        Assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        Assemblies.Add(Assembly.GetExecutingAssembly());
    }

    /// <summary>
    /// Adds MediatR services to the application with custom service registrations.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddMediator(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(conf => { conf.RegisterServicesFromAssemblies(Assemblies.ToArray()); });

        return builder;
    }

    /// <summary>
    /// Adds caching services to the web application builder.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddCaching(this WebApplicationBuilder builder)
    {
        // Configure CacheSettings from the app settings.
        builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection(nameof(CacheSettings)));

        // Configure Redis caching with options from the app settings.
        builder.Services.AddStackExchangeRedisCache(
            options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("RedisConnectionString");
                options.InstanceName = "AirBnb.CacheMemory";
            });

        // Register the RedisDistributedCacheBroker as a singleton.
        builder.Services.AddSingleton<ICacheBroker, RedisDistributedCacheBroker>();

        // register authentication handlers
        var jwtSettings = builder.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>() ??
                          throw new InvalidOperationException("JwtSettings is not configured.");

        // add authentication
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                options =>
                {
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = jwtSettings.ValidateIssuer,
                        ValidIssuer = jwtSettings.ValidIssuer,
                        ValidAudience = jwtSettings.ValidAudience,
                        ValidateAudience = jwtSettings.ValidateAudience,
                        ValidateLifetime = jwtSettings.ValidateLifetime,
                        ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                    };
                }
            );
        
        // Register middlewares
        builder.Services.AddSingleton<AccessTokenValidationMiddleware>();

        return builder;
    }

    ///<summary>
    /// Configures and adds Serializers to web application.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddSerializers(this WebApplicationBuilder builder)
    {
        // register json serialization settings
        builder.Services.AddSingleton<IJsonSerializationSettingsProvider, JsonSerializationSettingsProvider>();

        return builder;
    }
    
    /// <summary>
    /// Adds persistence-related services to the web application builder.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        // Register configurations
        builder.Services.Configure<DataAccessSettings>(builder.Configuration.GetSection(nameof(DataAccessSettings)));
        var dataAccessSettings = builder.Configuration.GetSection(nameof(DataAccessSettings)).Get<DataAccessSettings>()
                                 ?? throw new InvalidOperationException("Data access settings not found");
        
        // register ef interceptors

        //register db context
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            if(dataAccessSettings.UseInMemoryDatabase)
                options.UseInMemoryDatabase("InsightBox.Database");
            else
                options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnectionString"));
        });

        return builder;
    }

    /// <summary>
    /// Configures the Dependency Injection container to include validators from referenced assemblies.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblies(Assemblies);
        return builder;
    }

    /// <summary>
    /// Configures AutoMapper for object-to-object mapping using the specified profile.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(Assemblies);
        return builder;
    }

    /// <summary>
    /// Adds client-related infrastructure services to the web application builder.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddClientInfrastructure(this WebApplicationBuilder builder)
    {
        // Register repositories
        builder.Services
            .AddScoped<IClientRepository, ClientRepository>()
            .AddScoped<IOrganizationRepository, OrganizationRepository>()
            .AddScoped<IProductRepository, ProductRepository>();
        
        // Register services
        builder.Services
            .AddScoped<IClientService, ClientService>();

        return builder;
    }

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
    /// Seeds data into the application's database by creating a service scope and initializing the seed operation.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    private static async ValueTask<WebApplication> SeedDataAsync(this WebApplication app)
    {
        var serviceScope = app.Services.CreateScope();
        await serviceScope.ServiceProvider.InitializeSeedAsync();

        return app;
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