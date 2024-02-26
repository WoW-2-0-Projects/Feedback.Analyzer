using System.Reflection;
using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Persistence.DataContexts;

public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    //Db sets
    public DbSet<Client> Clients => Set<Client>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}