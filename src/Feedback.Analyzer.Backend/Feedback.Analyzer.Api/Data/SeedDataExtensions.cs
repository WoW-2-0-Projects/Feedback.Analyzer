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
    /// <summary>
    /// Initializes the seed data asynchronously.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
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

        if (!await appDbContext.Products.AnyAsync())
            await SeedProductsAsync(appDbContext);

        if (!await appDbContext.Feedbacks.AnyAsync())
            await SeedDataCustomerFeedbackAsync(appDbContext);

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

    /// <summary>
    /// Seeds organization data asynchronously.
    /// </summary>
    /// <param name="appDbContext"></param>
    private static async ValueTask SeedOrganizationAsync(AppDbContext appDbContext)
    {
        var organizations = new List<Organization>
        {
            new()
            {
                Id = Guid.Parse("c2fe1019-1180-4f3e-b477-413a9b33bbd1"),
                Name = "Najot Ta'lim",
                Description =
                    "Ushbu tashkilot o'z a'zolariga yangi texnologiyalar va dasturlash tillari bo'yicha sifatli ta'lim beradi. Maqsadimiz har bir o'quvchiga amaliy ko'nikmalar va chuqur bilim berish.",
                ClientId = Guid.Parse("54e16318-d140-4453-80c9-1a117dbe75fd"),
            },
            new()
            {
                Id = Guid.Parse("e57f81a1-1aeb-4f1c-aae0-9f0e1dcb92c4"),
                Name = "TechnoPark",
                Description =
                    "TechnoPark yosh innovatorlar va texnologiya ishqibozlari uchun mo'ljallangan markazdir Biz startaplar va texnologik loyihalar uchun qo'llab-quvvatlash va resurslar taqdim etamiz.",
                ClientId = Guid.Parse("54e16318-d140-4453-80c9-1a117dbe75fd"),
            },
            new()
            {
                Id = Guid.Parse("9d2aa9e2-362b-47f2-a46a-f328a0712d3d"),
                Name = "EduCenter",
                Description =
                    "EduCenter butun umr davomida ta'lim olishni qo'llab-quvvatlaydi.Biz turli yoshdagi odamlarga ko'nikmalarini oshirish va yangi sohalarni o'rganish imkoniyatini beramiz.",
                ClientId = Guid.Parse("5edbb0fe-7263-4f75-bad8-c9f3d422dd1d"),
            },
            new()
            {
                Id = Guid.Parse("60e6a4de-31e5-4f8b-8e6a-0a8f63f41527"),
                Name = "InnoCity",
                Description =
                    "InnoCity innovatsiya va tadbirkorlikni qo'llab-quvvatlaydigan shahar bo'lib, yosh tadbirkorlarga o'sish uchun zarur bo'lgan barcha sharoitlarni yaratadi.Biznes inkubatsiya dasturlari va moliyaviy yordam bizning asosiy xizmatlarimizdan biridir.",
                ClientId = Guid.Parse("5edbb0fe-7263-4f75-bad8-c9f3d422dd1d"),
            }
        };

        await appDbContext.Organizations.AddRangeAsync(organizations);
    }
    
    /// <summary>
    /// Seeds email template data asynchronously.
    /// </summary>
    /// <param name="appDbContext"></param>
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

    /// <summary>
    /// Seeds sms template data asynchronously.
    /// </summary>
    /// <param name="appDbContext"></param>
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


    /// <summary>
    /// Seeds product data asynchronously.
    /// </summary>
    /// <param name="appDbContext"></param>
    private static async ValueTask SeedProductsAsync(AppDbContext appDbContext)
    {
        var products = new List<Product>
        {
            new()
            {
                Id = Guid.Parse("751d1c24-24c2-45aa-9eba-383de543b34b"),
                OrganizationId = Guid.Parse("c2fe1019-1180-4f3e-b477-413a9b33bbd1"),
                Name = "iPhone",
                Description = "iPhone is very famous and expensive smart phone in the world. It made by Apple.",
            },
            new()
            {
                Id = Guid.Parse("1ca01475-d036-4ac3-a326-a2580110ee0c"),
                OrganizationId = Guid.Parse("e57f81a1-1aeb-4f1c-aae0-9f0e1dcb92c4"),
                Name = "Macbook",
                Description =
                    "Macbook is suitable for software developers, designer and etc. It is so expensive but people love it.",
            }
        };

        await appDbContext.Products.AddRangeAsync(products);
    }

    private static async ValueTask SeedDataCustomerFeedbackAsync(AppDbContext appDbContext)
    {
        var customersFeedbacks = new List<CustomerFeedback>()
        {
            // Positive feedback
            new()
            {
                ProductId = Guid.Parse("1ca01475-d036-4ac3-a326-a2580110ee0c"),
                Comment = "This product is **amazing**! It's **easy to use** and **exceeded my expectations**.",
                UserName = "John Doe",
            },

            // Neutral feedback
            new()
            {
                ProductId = Guid.Parse("751d1c24-24c2-45aa-9eba-383de543b34b"),
                Comment = "The product is **functional**. It **meets my basic needs**.",
                UserName = "Jane Smith",
            },

            // Negative feedback
            new()
            {
                ProductId = Guid.Parse("1ca01475-d036-4ac3-a326-a2580110ee0c"),
                Comment =
                    "I found the product to be **confusing** and **difficult to navigate**. It also **lacked some features** I was hoping for.",
                UserName = "Alice Miller",
            },

            // Anonymous feedback with mixed sentiment
            new()
            {
                ProductId = Guid.Parse("751d1c24-24c2-45aa-9eba-383de543b34b"),
                Comment = "The product has **some great features**, but it also has **some flaws**.",
                UserName = "Joane Miller",
            }
        };
        
        await appDbContext.Feedbacks.AddRangeAsync(customersFeedbacks);
    }
}