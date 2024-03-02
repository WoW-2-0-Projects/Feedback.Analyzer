using System.Reflection;
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

    #region NotiifcationInfrastructure
    public DbSet<SmsTemplate> SmsTemplates => Set<SmsTemplate>();

    public DbSet<SmsHistory> SmsHistories => Set<SmsHistory>();

    public DbSet<EmailTemplate> EmailTemplates => Set<EmailTemplate>();

    public DbSet<EmailHistory> EmailHistories => Set<EmailHistory>();
    #endregion
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}