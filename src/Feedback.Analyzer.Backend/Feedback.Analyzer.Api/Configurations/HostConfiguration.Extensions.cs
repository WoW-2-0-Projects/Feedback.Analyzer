using System.Reflection;
using Feedback.Analyzer.Api.Data;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Application.Common.Settings;
using Feedback.Analyzer.Application.Organizations.Services;
using Feedback.Analyzer.Application.Products.Services;
using Feedback.Analyzer.Domain.Constants;
using Feedback.Analyzer.Infrastructure.Clients.Services;
using Feedback.Analyzer.Infrastructure.Common.Prompts.Services;
using Feedback.Analyzer.Infrastructure.Common.Settings;
using Feedback.Analyzer.Infrastructure.Organizations.Services;
using Feedback.Analyzer.Infrastructure.Products.Services;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;

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
    /// Configures the Dependency Injection container to include validators from referenced assemblies.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ValidationSettings>(builder.Configuration.GetSection(nameof(ValidationSettings)));

        builder.Services.AddValidatorsFromAssemblies(Assemblies).AddFluentValidationAutoValidation();
        
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
    /// Adds persistence-related services to the web application builder.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        // define db connection string based on runtime environment
        var dbConnectionString = builder.Environment.IsProduction()
            ? Environment.GetEnvironmentVariable(DataAccessConstants.DbConnectionString)
            : builder.Configuration.GetConnectionString(DataAccessConstants.DbConnectionString);
        
        // register ef interceptors

        //register db context
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(dbConnectionString);
        });

        return builder;
    }
    
    /// <summary>
    /// Adds client-related infrastructure services to the web application builder.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns> </returns>
    private static WebApplicationBuilder AddClientInfrastructure(this WebApplicationBuilder builder)
    {
        // Register repositories
        builder.Services
            .AddScoped<IClientRepository, ClientRepository>()
            .AddScoped<IOrganizationRepository, OrganizationRepository>()
            .AddScoped<IProductRepository, ProductRepository>();
        
        // Register services
        builder.Services
            .AddScoped<IClientService, ClientService>()
            .AddScoped<IOrganizationService, OrganizationService>()
            .AddScoped<IProductService, ProductService>();

        return builder;
    }
    
        
    /// <summary>
    /// Configures exposers including controllers
    /// </summary>
    /// <param name="builder">Application builder</param>
    /// <returns></returns>
    private static WebApplicationBuilder AddSemanticKernelInfrastructure(this WebApplicationBuilder builder)
    {
        // Create kernel builder
        var kernelBuilder = Kernel.CreateBuilder();

        // Add OpenAI connector
        kernelBuilder.AddOpenAIChatCompletion(modelId: "gpt-3.5-turbo", apiKey: builder.Configuration["OpenAiApiSettings:ApiKey"]!);

        // Build kernel
        var kernel = kernelBuilder.Build();
        
        builder.Services.AddSingleton(kernel);

        return builder;
    }
    
    /// <summary>
    /// Configures exposers including controllers
    /// </summary>
    /// <param name="builder">Application builder</param>
    /// <returns></returns>
    private static WebApplicationBuilder AddPromptAnalysisInfrastructure(this WebApplicationBuilder builder)
    {
        // Register repositories
        builder.Services.AddScoped<IPromptRepository, PromptRepository>();
        
        // Register services
        builder.Services.AddScoped<IPromptService, PromptService>();

        return builder;
    }
    
    /// <summary>
    /// Adds MediatR services to the application with custom service registrations.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddMediator(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(conf => {conf.RegisterServicesFromAssemblies(Assemblies.ToArray());});
        
        return builder;
    }
    
    /// <summary>
    /// Configures CORS for the web application.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        // Register settings
        builder.Services.Configure<CorsSettings>(builder.Configuration.GetSection(nameof(CorsSettings)));
        var corsSettings = builder.Configuration.GetSection(nameof(CorsSettings)).Get<CorsSettings>()
            ?? throw new ApplicationException("Cors settings are not configured");
        
        builder.Services.AddCors(options => options.AddPolicy("AllowSpecificOrigin",
            policy =>
            {
                policy.WithOrigins(corsSettings.AllowedOrigins);
                    
                if(corsSettings.AllowAnyHeaders)
                   policy.AllowAnyHeader();
                
                if(corsSettings.AllowAnyMethods)
                    policy.AllowAnyMethod();
                
                if(corsSettings.AllowCredentials)
                    policy.AllowCredentials();
            }
        ));

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
    ///  Configures exposers including controllers and routing.
    /// </summary>
    /// <param name="builder">>Application builder</param>
    /// <returns>Application builder</returns>
    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ApiBehaviorOptions>(
            options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            }
        );
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers().AddNewtonsoftJson();
        
        return builder;
    }
    
    /// <summary>
    /// Asynchronously migrates database schemas associated with the application.
    /// </summary>
    /// <param name="app">The WebApplication instance to configure.</param>
    /// <returns>A ValueTask representing the asynchronous operation, with the WebApplication instance.</returns>
    private static async ValueTask<WebApplication> MigrateDataBaseSchemasAsync(this WebApplication app)
    {
        var serviceScopeFactory = app.Services.GetRequiredKeyedService<IServiceScopeFactory>(null);
        
        await serviceScopeFactory.MigrateAsync<AppDbContext>();
        
        return app;
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
    /// Enables CORS middleware in the web application pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    private static WebApplication UseCors(this WebApplication app)
    {
        app.UseCors("AllowSpecificOrigin");

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
}