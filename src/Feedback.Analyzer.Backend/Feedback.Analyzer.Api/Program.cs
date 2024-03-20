using Feedback.Analyzer.Api.Configurations;
using Feedback.Analyzer.Domain.Enums;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
await builder.ConfigureAsync();

var app = builder.Build();

await app.ConfigureAsync();
await app.RunAsync();