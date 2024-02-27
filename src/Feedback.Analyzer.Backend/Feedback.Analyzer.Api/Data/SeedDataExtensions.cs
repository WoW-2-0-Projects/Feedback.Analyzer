using Bogus;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Api.Data;

/// <summary>
/// Extension methods for seeding data into the database.
/// </summary>
public static class SeedDataExtensions
{
    public static async ValueTask InitializeSeedAsync(this IServiceProvider serviceProvider)
    {
        var appDbContext = serviceProvider.GetRequiredService<AppDbContext>();

        if (!await appDbContext.Clients.AnyAsync())
            await appDbContext.SeedClientsAsync();

        if (appDbContext.ChangeTracker.HasChanges())
            await appDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Seeds client data asynchronously.
    /// </summary>
    /// <param name="dbContext">The application database context.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private static async ValueTask SeedClientsAsync(this AppDbContext dbContext)
    {
        var client1 = new Client()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "example@gmail.com",
            Password = "abc1234567"
        };

        var client2 = new Client()
        {
            FirstName = "Bob",
            LastName = "Richard",
            Email = "tastBobRichard@gmail.com",
            Password = "asdf1234"
        };

        await dbContext.Clients.AddAsync(client1);
        await dbContext.Clients.AddAsync(client2);
    }
}