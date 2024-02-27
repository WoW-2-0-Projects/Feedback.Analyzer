using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using FluentValidation;

namespace Feedback.Analyzer.Infrastructure.Clients.Validators;

public class ClientValidator : AbstractValidator<Client>
{
    public ClientValidator()
    {
        RuleSet(EntityEvent.OnCreate.ToString(), () => 
        {
            RuleFor(client => client.FirstName).NotEmpty().MaximumLength(64);
            
            RuleFor(client => client.LastName).NotEmpty().MaximumLength(64);

            RuleFor(client => client.EmailAddress).NotEmpty().MaximumLength(128);
            
            RuleFor(client => client.Password).NotEmpty().MaximumLength(128);
        });
        
        RuleSet(EntityEvent.OnUpdate.ToString(), () => 
        {
            RuleFor(client => client.FirstName).NotEmpty().MaximumLength(64);
            
            RuleFor(client => client.LastName).NotEmpty().MaximumLength(64);

            RuleFor(client => client.EmailAddress).NotEmpty().MaximumLength(128);
            
            RuleFor(client => client.Password).NotEmpty().MaximumLength(128);
        });
    }
}