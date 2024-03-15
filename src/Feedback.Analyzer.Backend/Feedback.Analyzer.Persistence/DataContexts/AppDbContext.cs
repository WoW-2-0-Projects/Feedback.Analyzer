using System.Reflection;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Persistence.EntityConfiguration;
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

    public DbSet<FeedbackAnalysisResult> FeedbackAnalysisResults => Set<FeedbackAnalysisResult>();

    #endregion

    #region PromptsInfrastructure

    public DbSet<PromptExecutionHistory> PromptExecutionHistories => Set<PromptExecutionHistory>();

    #endregion
    
    #region Prompt infrastructure
    public DbSet<AnalysisPrompt> Prompts => Set<AnalysisPrompt>();

    public DbSet<AnalysisWorkflow> AnalysisWorkflows => Set<AnalysisWorkflow>();
    
    public DbSet<WorkflowExecutionOption> WorkflowExecutionOptions => Set<WorkflowExecutionOption>();
    
    public DbSet<AnalysisPromptCategory> PromptCategories => Set<AnalysisPromptCategory>();

    public DbSet<FeedbackAnalysisWorkflow> FeedbackAnalysisWorkflows => Set<FeedbackAnalysisWorkflow>();
    #endregion
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}