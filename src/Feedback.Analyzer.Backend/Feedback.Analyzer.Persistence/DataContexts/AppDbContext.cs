using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Persistence.DataContexts;

/// <summary>
/// Initializes a new instance of the <see cref="AppDbContext"/> class with the specified options.
/// </summary>
/// <param name="dbContextOptions">The options for configuring the database context.</param>
public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    //Db sets
    
    /// <summary>
    /// Configures the model using the provided model builder.
    /// </summary>
    /// <param name="modelBuilder">The model builder to use for configuration.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}