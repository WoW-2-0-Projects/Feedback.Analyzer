using Feedback.Analyzer.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

await builder.ConfigureAsync();

var app = builder.Build();

Console.WriteLine("Hello");

await app.ConfigureAsync();
await app.RunAsync();