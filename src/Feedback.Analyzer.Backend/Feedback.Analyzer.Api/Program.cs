using Feedback.Analyzer.Api.Configurations;

var executionTimes = new List<TimeSpan>
{
    TimeSpan.FromSeconds(3),
    TimeSpan.FromSeconds(4),
    TimeSpan.FromSeconds(5),
    TimeSpan.FromSeconds(1),
};

var avg = executionTimes.Average(timeSpan => timeSpan.TotalMilliseconds);

// executionTimes[0].Duration()

// var average = new TimeSpan((long)executionTimes.Select(time => time.Ticks).Average()).Seconds;

// opt.MapFrom(src => new TimeSpan((long)src.ExecutionHistories.Select(history => history.ExecutionTime.Ticks).Average()).Seconds);

var builder = WebApplication.CreateBuilder(args);

await builder.ConfigureAsync();

var app = builder.Build();

await app.ConfigureAsync();
await app.RunAsync();