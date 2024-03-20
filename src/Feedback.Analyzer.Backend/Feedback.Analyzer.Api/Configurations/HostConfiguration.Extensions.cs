using System.Reflection;
using System.Text;
using Feedback.Analyzer.Api.Data;
using Feedback.Analyzer.Api.Middlewares;
using Feedback.Analyzer.Application.Clients.Services;
using Feedback.Analyzer.Application.Common.AnalysisWorkflows.Services;
using Feedback.Analyzer.Application.Common.Caching;
using Feedback.Analyzer.Application.Common.EventBus.Brokers;
using Feedback.Analyzer.Application.Common.Identity.Services;
using Feedback.Analyzer.Application.Common.PromptCategory.Services;
using Feedback.Analyzer.Application.Common.Prompts.Brokers;
using Feedback.Analyzer.Application.Common.Prompts.Services;
using Feedback.Analyzer.Application.Common.PromptsHistory.Services;
using Feedback.Analyzer.Application.Common.Serializers;
using Feedback.Analyzer.Application.Common.Settings;
using Feedback.Analyzer.Application.Common.WorkflowExecutionOptions.Services;
using Feedback.Analyzer.Application.CustomerFeedbacks.Services;
using Feedback.Analyzer.Application.FeedbackAnalysisResults.Services;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflowResults.Services;
using Feedback.Analyzer.Application.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Domain.Brokers;
using Feedback.Analyzer.Application.Organizations.Services;
using Feedback.Analyzer.Application.Products.Services;
using Feedback.Analyzer.Domain.Constants;
using Feedback.Analyzer.Infrastructure.Clients.Services;
using Feedback.Analyzer.Infrastructure.Common.AnalysisWorkflows.Services;
using Feedback.Analyzer.Infrastructure.Common.EventBus.Brokers;
using Feedback.Analyzer.Infrastructure.Common.Identity.Services;
using Feedback.Analyzer.Infrastructure.Common.Prompts.Brokers;
using Feedback.Analyzer.Infrastructure.Common.Prompts.Services;
using Feedback.Analyzer.Infrastructure.Common.PromptsCategories.Services;
using Feedback.Analyzer.Infrastructure.Common.PromptsHistory.Services;
using Feedback.Analyzer.Infrastructure.Common.RequestContexts.Brokers;
using Feedback.Analyzer.Infrastructure.Common.Serializers;
using Feedback.Analyzer.Infrastructure.Common.Settings;
using Feedback.Analyzer.Infrastructure.Common.WorkflowExecutionOptions.Services;
using Feedback.Analyzer.Persistence.Caching.Brokers;
using Feedback.Analyzer.Infrastructure.Organizations.Services;
using Feedback.Analyzer.Infrastructure.Products.Services;
using Feedback.Analyzer.Infrastructure.CustomerFeedbacks.Services;
using Feedback.Analyzer.Infrastructure.FeedbackAnalysisResults.Services;
using Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflowResults.Services;
using Feedback.Analyzer.Infrastructure.FeedbackAnalysisWorkflows.Services;
using Feedback.Analyzer.Persistence.DataContexts;
using Feedback.Analyzer.Persistence.Repositories;
using Feedback.Analyzer.Persistence.Repositories.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SemanticKernel;
using MassTransit;
using Feedback.Analyzer.Infrastructure.Common.Workflows.EventHandlers;
using Feedback.Analyzer.Infrastructure.Common.Identity.EventHandlers;

namespace Feedback.Analyzer.Api.Configurations;

public static partial class HostConfiguration
{
    private static readonly ICollection<Assembly> Assemblies;

    static HostConfiguration()
    {
        Assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        Assemblies.Add(Assembly.GetExecutingAssembly());
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
    /// Adds caching services to the web application builder.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddCaching(this WebApplicationBuilder builder)
    {
        // Configure CacheSettings from the app settings.
        builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection(nameof(CacheSettings)));

        // Configure Redis caching with options from the app settings.
        // builder.Services.AddStackExchangeRedisCache(
        //     options =>
        //     {
        //         options.Configuration = builder.Configuration.GetConnectionString("RedisConnectionString");
        //         options.InstanceName = "AirBnb.CacheMemory";
        //     });

        builder.Services.AddMemoryCache();
        
        // Register the Memory Cache as a singleton.
        builder.Services.AddSingleton<ICacheBroker, MemoryCacheBroker>();
        
        // Register middlewares
        builder.Services.AddSingleton<AccessTokenValidationMiddleware>();

        return builder;
    }

    /// <summary>
    /// Extension method for adding event bus services to the application.
    /// </summary>
    private static WebApplicationBuilder AddEventBus(this WebApplicationBuilder builder)
    {
        builder
           .Services
           .AddMassTransit(configuration =>
           {
               configuration
               .AddConsumersFromNamespaceContaining<ExecuteWorkflowSinglePromptEventHandler>();

               configuration
               .AddConsumersFromNamespaceContaining<UserCreatedEventHandler>();

               configuration.UsingInMemory((context, cfg) =>
               {
                   cfg.ConfigureEndpoints(context);
               });
           });

        builder.Services.AddSingleton<IEventBusBroker, MassTransitEventBusBroker>();

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
    /// Configures devTools including controllers
    /// Configures IdentityInfrastructure including controllers
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        //configuration settings
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
        builder.Services.Configure<PasswordValidationSettings>(builder.Configuration.GetSection(nameof(PasswordValidationSettings)));

        //register repository
        builder.Services
            .AddScoped<IAccessTokenRepository, AccessTokenRepository>()
            .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        
        //register services
        builder.Services
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IIdentitySecurityTokenGeneratorService, IdentitySecurityTokenGeneratorService>()
            .AddScoped<IIdentitySecurityTokenService, IdentitySecurityTokenService>()
            .AddScoped<IPasswordGeneratorService, PasswordGeneratorService>()
            .AddScoped<IPasswordHasherService, PasswordHasherService>();
        
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
    /// Configures the Dependency Injection container to include validators from referenced assemblies.
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
    /// Adds feedback-related infrastructure services to the web application builder.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddFeedbackInfrastructure(this WebApplicationBuilder builder)
    {
        // Register repositories
        builder.Services
            .AddScoped<ICustomerFeedbackRepository, CustomerFeedbackRepository>()
            .AddScoped<IFeedbackAnalysisResultRepository, FeedbackAnalysisResultRepository>()
            .AddScoped<IFeedbackAnalysisWorkflowResultRepository, FeedbackAnalysisWorkflowResultRepository>();
        
        // Register services
        builder.Services
            .AddScoped<ICustomerFeedbackService, CustomerFeedbackService>()
            .AddScoped<IFeedbackAnalysisResultService, FeedbackAnalysisResultService>()
            .AddScoped<IFeedbackAnalysisWorkflowResultService, FeedbackAnalysisWorkflowResultService>()
            .AddScoped<IFeedbackAnalysisOrchestrationService, FeedbackAnalysisOrchestrationService>()
            .AddScoped<IFeedbackBatchAnalysisOrchestrationService, FeedbackBatchAnalysisOrchestrationService>();

        return builder;
    }
    
    /// <summary>
    /// Adds Semantic Analysis infrastructure to the web application builder.
    /// Registers brokers, repositories, foundation services, and processing services required for semantic analysis.
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> to which the services are added.</param>
    /// <returns>The same instance of the <see cref="WebApplicationBuilder"/> for chaining.</returns>
    private static WebApplicationBuilder AddSemanticAnalysisInfrastructure(this WebApplicationBuilder builder)
    {
        // Register brokers
        builder.Services
               .AddScoped<IPromptExecutionBroker, PromptExecutionBroker>();
        
        // Register repositories
        builder.Services
            .AddScoped<IPromptRepository, PromptRepository>()
            .AddScoped<IPromptCategoryRepository, PromptCategoryRepository>()
            .AddScoped<IPromptExecutionHistoryRepository, PromptExecutionHistoryRepository>()
            .AddScoped<IAnalysisWorkflowRepository, AnalysisWorkflowRepository>()
            .AddScoped<IFeedbackAnalysisWorkflowRepository, FeedbackAnalysisWorkflowRepository>()
            .AddScoped<IWorkflowExecutionOptionRepository, WorkflowExecutionOptionRepository>(); 

        // Register foundation services
        builder.Services
            .AddScoped<IPromptService, PromptService>()
            .AddScoped<IPromptCategoryService, PromptCategoryService>()
            .AddScoped<IPromptExecutionHistoryService, PromptExecutionHistoryService>()
            .AddScoped<IAnalysisWorkflowService, AnalysisWorkflowService>()
            .AddScoped<IWorkflowExecutionOptionsService, WorkflowExecutionOptionsService>()
            .AddScoped<IAnalysisWorkflowService, AnalysisWorkflowService>()
            .AddScoped<IFeedbackAnalysisWorkflowService, FeedbackAnalysisWorkflowService>()
            .AddScoped<IFeedbackAnalysisWorkflowProcessingService, FeedbackAnalysisWorkflowProcessingService>();

        // Register processing services
        builder.Services
               .AddScoped<IPromptExecutionProcessingService, PromptExecutionProcessingService>();
        
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
    /// Configures Request Context tool for the web application.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddRequestContextTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddScoped<IRequestContextProvider, RequestContextProvider>();

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
    /// Adds client-related infrastructure services to the web application builder.
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