using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Persistence.DataContexts;

public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    #region Semantic Analysis Infrastructure

    public DbSet<AnalysisPromptCategory> PromptCategories => Set<AnalysisPromptCategory>();
    
    public DbSet<AnalysisPrompt> Prompts => Set<AnalysisPrompt>();
    
    public DbSet<PromptExecutionHistory> PromptExecutionHistories => Set<PromptExecutionHistory>();

    public DbSet<AnalysisWorkflow> AnalysisWorkflows => Set<AnalysisWorkflow>();
    
    public DbSet<WorkflowExecutionOption> WorkflowExecutionOptions => Set<WorkflowExecutionOption>();
    
    #endregion
    
    #region Clients Infrastructure
   
    public DbSet<Client> Clients => Set<Client>();
    
    public DbSet<Organization> Organizations => Set<Organization>();

    public DbSet<Product> Products => Set<Product>();
    
    #endregion

    #region Feedbacks Analysis Infrastructure
    
    public DbSet<CustomerFeedback> Feedbacks => Set<CustomerFeedback>();

    public DbSet<FeedbackAnalysisResult> FeedbackAnalysisResults => Set<FeedbackAnalysisResult>();
    
    public DbSet<FeedbackAnalysisWorkflowResult> FeedbackAnalysisWorkflowResults => Set<FeedbackAnalysisWorkflowResult>();
    
    public DbSet<FeedbackAnalysisWorkflow> FeedbackAnalysisWorkflows => Set<FeedbackAnalysisWorkflow>();
    
    public DbSet<FeedbackAnalysisWorkflowResultStats> FeedbackAnalysisWorkflowResultStats => Set<FeedbackAnalysisWorkflowResultStats>();
    
    public DbSet<FeedbackAnalysisWorkflowResultPoint> FeedbackAnalysisWorkflowResultPoints => Set<FeedbackAnalysisWorkflowResultPoint>();

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}