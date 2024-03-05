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

    #endregion

    #region Prompts Infrastructure
    
    public DbSet<AnalysisPrompt> Prompts => Set<AnalysisPrompt>();
    
    public DbSet<AnalysisPromptCategory> PromptCategories => Set<AnalysisPromptCategory>();
    
    public DbSet<FeedbackAnalysisResult> FeedbackAnalysisResults => Set<FeedbackAnalysisResult>();
    
    public DbSet<FeedbackExecutionWorkflow> FeedbackExecutionWorkflows => Set<FeedbackExecutionWorkflow>();
    
    public DbSet<PromptExecutionHistory> PromptExecutionHistories => Set<PromptExecutionHistory>();
    
    #endregion
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}