using Feedback.Analyzer.Domain.Common.Prompts;
using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Persistence.DataContexts;

public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    #region ClientsInfrastructure
   
    public DbSet<Client> Clients => Set<Client>();
    
    public DbSet<Organization> Organizations => Set<Organization>();

    public DbSet<Product> Products => Set<Product>();
    
    #endregion

    #region FeedbackInfrastructure
    
    public DbSet<CustomerFeedback> Feedbacks => Set<CustomerFeedback>();

    public DbSet<AnalysisPrompt> Prompts => Set<AnalysisPrompt>();
    
    public DbSet<AnalysisPromptCategory> PromptCategories => Set<AnalysisPromptCategory>();

    #endregion
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}