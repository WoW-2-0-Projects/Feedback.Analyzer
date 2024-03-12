using Feedback.Analyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Feedback.Analyzer.Persistence.EntityConfiguration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.Property(client => client.FirstName).HasMaxLength(64).IsRequired();
        builder.Property(client => client.LastName).HasMaxLength(64).IsRequired();
        builder.Property(client => client.EmailAddress).HasMaxLength(128).IsRequired();
        builder.Property(client => client.PasswordHash).HasMaxLength(128).IsRequired();
    }
}