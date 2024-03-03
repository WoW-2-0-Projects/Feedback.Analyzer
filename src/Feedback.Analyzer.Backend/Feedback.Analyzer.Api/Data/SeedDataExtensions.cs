using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
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

        if (!await appDbContext.Organizations.AnyAsync())
            await SeedOrganizationAsync(appDbContext);

        if (!await appDbContext.EmailTemplates.AnyAsync())
            await appDbContext.SeedEmailTemplates(serviceProvider.GetRequiredService<IWebHostEnvironment>());

        if (!await appDbContext.SmsTemplates.AnyAsync())
            await appDbContext.SeedSmsTemplates();

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
        var clients = new List<Client>
        {
            new()
            {  
                Id = Guid.Parse("54e16318-d140-4453-80c9-1a117dbe75fd"),
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "example@gmail.com",
                Password = "abc1234567"
            },
            new()
            {
                Id = Guid.Parse("5edbb0fe-7263-4f75-bad8-c9f3d422dd1d"),
                FirstName = "Bob",
                LastName = "Richard",
                EmailAddress = "tastBobRichard@gmail.com",
                Password = "asdf1234"
            }
        };

        await dbContext.Clients.AddRangeAsync(clients);
    }

    private static async ValueTask SeedOrganizationAsync(AppDbContext appDbContext)
    {
        var organizations = new List<Organization>
        {
            new ()
            {
                Name = "Najot Ta'lim",
                Description = "Ushbu tashkilot o'z a'zolariga yangi texnologiyalar va dasturlash tillari bo'yicha sifatli ta'lim beradi. Maqsadimiz har bir o'quvchiga amaliy ko'nikmalar va chuqur bilim berish.",
                ClientId = Guid.Parse("54e16318-d140-4453-80c9-1a117dbe75fd")
                    
            },
            new ()
            {
                Name = "TechnoPark",
                    
                Description = "TechnoPark yosh innovatorlar va texnologiya ishqibozlari uchun mo'ljallangan markazdir Biz startaplar va texnologik loyihalar uchun qo'llab-quvvatlash va resurslar taqdim etamiz.",
                ClientId = Guid.Parse("54e16318-d140-4453-80c9-1a117dbe75fd")
            },
            new ()
            {
                Name = "EduCenter",
                Description =
                    "EduCenter butun umr davomida ta'lim olishni qo'llab-quvvatlaydi.Biz turli yoshdagi odamlarga ko'nikmalarini oshirish va yangi sohalarni o'rganish imkoniyatini beramiz.",
                ClientId = Guid.Parse("5edbb0fe-7263-4f75-bad8-c9f3d422dd1d")
            },
            new ()
            {
                Name = "InnoCity",
                Description =
                    "InnoCity innovatsiya va tadbirkorlikni qo'llab-quvvatlaydigan shahar bo'lib, yosh tadbirkorlarga o'sish uchun zarur bo'lgan barcha sharoitlarni yaratadi.Biznes inkubatsiya dasturlari va moliyaviy yordam bizning asosiy xizmatlarimizdan biridir.",
                ClientId = Guid.Parse("5edbb0fe-7263-4f75-bad8-c9f3d422dd1d")
            }
        };

        await appDbContext.Organizations.AddRangeAsync(organizations);
    }

    private static async ValueTask SeedEmailTemplates(
        this AppDbContext appDbContext,
        IWebHostEnvironment webHostEnvironment
    )
    {
        var emailTemplateTypes = new List<NotificationTemplateType>
        {
            NotificationTemplateType.WelcomeNotification,
            NotificationTemplateType.EmailAddressVerificationNotification,
            NotificationTemplateType.ReferralNotification
        };

        var emailTemplateContents = await Task.WhenAll(emailTemplateTypes.Select(async templateType =>
        {
            var filePath = Path.Combine(webHostEnvironment.ContentRootPath,
                "Data",
                "EmailTemplates",
                Path.ChangeExtension(templateType.ToString(), "html"));
            return (TemplateType: templateType, TemplateContent: await File.ReadAllTextAsync(filePath));
        }));

        var emailTemplates = emailTemplateContents.Select(templateContent => templateContent.TemplateType switch
        {
            NotificationTemplateType.WelcomeNotification => new EmailTemplate
            {
                TemplateType = templateContent.TemplateType,
                Subject = "Welcome to our service!",
                Content = templateContent.TemplateContent
            },
            NotificationTemplateType.EmailAddressVerificationNotification => new EmailTemplate
            {
                TemplateType = templateContent.TemplateType,
                Subject = "Confirm your email address",
                Content = templateContent.TemplateContent
            },
            NotificationTemplateType.ReferralNotification => new EmailTemplate
            {
                TemplateType = templateContent.TemplateType,
                Subject = "You have been referred!",
                Content = templateContent.TemplateContent
            },
            _ => throw new NotSupportedException("Template type not supported.")
        });

        await appDbContext.EmailTemplates.AddRangeAsync(emailTemplates);
    }

    private static async ValueTask SeedSmsTemplates(this AppDbContext appDbContext)
    {
        await appDbContext.SmsTemplates.AddRangeAsync(new SmsTemplate
        {
            TemplateType = NotificationTemplateType.WelcomeNotification,
            Content =
                    "Welcome {{UserName}}! We're thrilled to have you on board. Get ready to explore and enjoy our services"
        },
            new SmsTemplate
            {
                TemplateType = NotificationTemplateType.PhoneNumberVerificationNotification,
                Content =
                    "Hey {{UserName}}. To secure your account, please verify your phone number using this link: {{PhoneNumberVerificationLink}}"
            },
            new SmsTemplate
            {
                TemplateType = NotificationTemplateType.ReferralNotification,
                Content =
                    "You've been invited to join by a friend {{SenderName}}! Sign up today and enjoy exclusive benefits. Use referral code"
            });
    }
}    