using Feedback.Analyzer.Domain.Entities;
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

        if (!await appDbContext.Products.AnyAsync())
            await SeedProductsAsync(appDbContext);

        if (!await appDbContext.Feedbacks.AnyAsync())
            await SeedDataCustomerFeedbackAsync(appDbContext);
        
        if (!await appDbContext.Prompts.AnyAsync())
            await SeedAnalysisPromptAsync(appDbContext);

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

    /// <summary>
    /// Seeds prompt categories
    /// </summary>
    /// <param name="appDbContext"></param>
    private static async ValueTask SeedAnalysisPromptAsync(AppDbContext appDbContext)
    {
        var analysisPrompts = new List<AnalysisPrompt>()
        {
            new()
            {
                Id = Guid.Parse("1ca01475-d036-4ac3-a326-a2580110ee0c"),
                Prompt = """
                         ## Instructions"

                         Analyze user feedback and provide a relevance with the service in true or false format

                         Conditions :
                         1. feedback must include at least 1 sentence about the service
                         2. even feedbacks that have non-related content counts if the rule 1 is satisfied

                         ## Data

                         {{$productDescription}}

                         ## Input

                         {{$customerFeedback}}

                         ## Result

                         """,
                Version = 0,
                Revision = 1,
            },
            new()
            {
                Id = Guid.Parse("3ca01475-d736-4ac3-a326-a2580110ee0c"),
                Prompt = """
                         ## Instructions"

                         Extract only relevant parts of the customer feedback for the product

                         Requirements :
                         1. if feedback contains relevant content in different parts of the feedback, all relevant parts must be extracted and appended
                         2. try to extract as a readable sentence, not just words

                         ## Data

                         {{$productDescription}}

                         ## Input

                         {{$customerFeedback}}

                         ## Result

                         """,
                Version = 1,
                Revision = 2,
            },
            new()
            {
                Id = Guid.Parse("4ca01475-d036-4ac3-a326-a2580110ee0c"),
                Prompt = """
                         ## Instructions"

                         Redact personal information from the customer feedback

                         Requirements :
                         1. redact only words that is considered as personal information, not the whole sentence
                         2. replace the redacted words with asterisks
                         3. make sure sentences are still readable

                         ## Data

                         {{$productDescription}}

                         ## Input

                         {{$customerFeedback}}

                         ## Result

                         """,
                Version = 2,
                Revision = 3,
            },
            new()
            {
                Id = Guid.Parse("2ba01475-d636-4ac3-a326-a2580112ee0c"),
                Prompt = """
                         ## Instructions"

                         Recognize languages from the customer feedback

                         Requirements :
                         1. list language if something readable or like a sentence written in it, not just a word
                         2. list all languages feedback contains multiple languages

                         ## Data

                         {{$productDescription}}

                         ## Input

                         {{$customerFeedback}}

                         ## Result

                         """,
                Version = 3,
                Revision = 4,
            },
            new()
            {
                Id = Guid.Parse("551d1c24-24c2-45aa-9eba-383de543b24b"),
                Prompt = """
                         ## Instructions"

                         Extract positive and  negative opinion points from the user feedback.

                         Requirements :
                         1. extract positive and negative opinion points from the user feedback.
                         2. don't include neutral opinion points
                         3. only extract opinion points that are related to the product
                         4. only extract the section that contains opinion itself not the whole sentence
                         5. don't include any points from product description
                         6. be aware of mixed complex sentences that might contain turning points
                         7. exclude actionable opinions because we want exact source of positive and negative experience not solutions
                         8. analyze and exclude sentences with opinions that you are not sure whether it is about this product
                         9. separate points if there are multiple points in a single sentence or in a conjunction

                         ## Examples

                         These examples contain turning points

                         - Overall I think the Viper is a good mouse, but I can't afford but - positive
                         - Overall I think the Viper is a good mouse, but not for me - neutral
                         - Overall Viper is a good mouse, they said, but nope - negative

                         ## Product Description:

                         {{$productDescription}}

                         ## Customer feedback :

                         {{$customerFeedback}}

                         ## Result

                         """,
                Version = 4,
                Revision = 5,
            }
        };

        await appDbContext.Prompts.AddRangeAsync(analysisPrompts);
    }
}