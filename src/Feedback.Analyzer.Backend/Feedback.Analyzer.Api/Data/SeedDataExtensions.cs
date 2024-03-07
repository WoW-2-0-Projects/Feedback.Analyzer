using Feedback.Analyzer.Domain.Constants;
using Feedback.Analyzer.Domain.Entities;
using Feedback.Analyzer.Domain.Enums;
using Feedback.Analyzer.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders.Testing;

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

        if (!await appDbContext.PromptCategories.AnyAsync())
            await SeedPromptCategoriesAsync(appDbContext);

        if (!await appDbContext.Prompts.AnyAsync())
            await SeedAnalysisPromptAsync(appDbContext);

        // if (!await appDbContext.PromptExecutionHistories.AnyAsync())
        //     await SeedAnalysisPromptAsync(appDbContext);

        if (!await appDbContext.FeedbackExecutionWorkflows.AnyAsync())
            await SeedAnalysisWorkflows(appDbContext);

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
            },
            new()
            {
                Id = Guid.Parse("18C7ACA8-6592-4E81-8AFF-F36FA596CC5A"),
                FirstName = "Sarah",
                LastName = "Func",
                EmailAddress = "test@gmail.com",
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
            },
            new()
            {
                Id = Guid.Parse("9DF2560B-17A0-456A-B858-B3C67ECCAEB2"),
                Name = "Razer Inc.",
                Description =
                    "Razer Inc is a multinational technology company specializing in the design, development, and distribution of gamer-focused hardware, software, and services. Headquartered in Singapore and California, Razer has established a strong global presence with a reputation for innovation and a dedication to the gaming community. Beyond its iconic gaming peripherals, the company offers powerful Blade laptops, immersive software solutions, and even participates in fintech services for youth and millennials.",
                ClientId = Guid.Parse("18C7ACA8-6592-4E81-8AFF-F36FA596CC5A"),
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
                Description = "Macbook is suitable for software developers, designer and etc. It is so expensive but people love it.",
            },
            new()
            {
                Id = Guid.Parse("46E96B3C-4028-4FD5-B38A-981237BD6F9D"),
                OrganizationId = Guid.Parse("9DF2560B-17A0-456A-B858-B3C67ECCAEB2"),
                Name = "Razer Viper Ultimate",
                Description = """
                              Razor Viper Ultimate - wireless gaming mouse
                                                  
                                                  Sensitivity: 20,000DPI
                                                  Tracking Speed : 650IPS
                                                  Resolution accuracy : 99.6%
                                                  
                                                  74G LIGHTWEIGHT DESIGN
                                                  Enjoy faster and smoother control with a lightweight wireless mouse designed for esports. Weighing just 74g, it achieves its weight without compromising on the build strength of its ambidextrous form factor.
                                                  
                                                  70 HOURS OF BATTERY LIFE
                                                  Improved wireless power efficiency keeps it running at peak performance for up to 70 continuous hours—charge it just once a week to power 10 hours of daily gameplay.
                                                  
                                                  5 ON-BOARD MEMORY PROFILES
                                                  Bring your settings anywhere and be match-ready in no time. Activate up to 5 profile configurations from its onboard memory or custom settings via cloud storage.
                                                  
                                                  8 PROGRAMMABLE BUTTONS
                                                  Fully configurable via Razer Synapse 3, the 8 programmable buttons let you access macros and secondary functions so you can execute extended moves with ease.,
                              """
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
                Comment = "I found the product to be **confusing** and **difficult to navigate**. It also **lacked some features** I was hoping for.",
                UserName = "Alice Miller",
            },

            // Anonymous feedback with mixed sentiment
            new()
            {
                ProductId = Guid.Parse("751d1c24-24c2-45aa-9eba-383de543b34b"),
                Comment = "The product has **some great features**, but it also has **some flaws**.",
                UserName = "Joane Miller",
            },
            
            new()
            {
                ProductId = Guid.Parse("46E96B3C-4028-4FD5-B38A-981237BD6F9D"),
                Comment = "I laid my hands on the Viper in a local store and on the spot it felt rather flat. I think I prefer something with more hump and side area to grip onto. right now I have a basilisk v2 and think it's definetely more comfy, but that grip could still be better.\n\nThe Synapse software is extremely greedy though. 300MB HD space and about 200MB RAM is ludicrous for a gloryfied mouse driver.",
                UserName = "Silvarspark"
            }
        };

        await appDbContext.Feedbacks.AddRangeAsync(customersFeedbacks);
    }

    /// <summary>
    /// Seeds prompt categories
    /// </summary>
    /// <param name="appDbContext"></param>
    private static async ValueTask SeedPromptCategoriesAsync(AppDbContext appDbContext)
    {
        var promptCategories = new List<AnalysisPromptCategory>
        {
            new()
            {
                Id = Guid.Parse("15072FC8-63C7-49EC-BF4F-3FD2A8479CF4"),
                Category = FeedbackAnalysisPromptCategory.ContentSafetyAnalysis,
            },
            new()
            {
                Id = Guid.Parse("7397EB27-EEAF-4898-9B0C-D78613817C30"),
                Category = FeedbackAnalysisPromptCategory.RelevanceAnalysis,
            },
            new()
            {
                Id = Guid.Parse("28C2137D-E6F7-440D-9513-1EE2E0B36530"),
                Category = FeedbackAnalysisPromptCategory.LanguageRecognition,
            },
            new()
            {
                Id = Guid.Parse("787BB696-5057-4840-9161-770AD88FFA9B"),
                Category = FeedbackAnalysisPromptCategory.RelevantContentExtraction,
            },
            new()
            {
                Id = Guid.Parse("159D0655-40AE-4DED-8C83-0FFFF69A7704"),
                Category = FeedbackAnalysisPromptCategory.EntityIdentification,
            },
            new()
            {
                Id = Guid.Parse("FD49A0B2-403F-491F-A4C4-1C489758FB79"),
                Category = FeedbackAnalysisPromptCategory.PersonalInformationRedaction,
            },
            new()
            {
                Id = Guid.Parse("D187624D-8AF7-4495-BF7B-00084A63372E"),
                Category = FeedbackAnalysisPromptCategory.OpinionMining,
            },
            new()
            {
                Id = Guid.Parse("B12F3C18-2706-42BB-BF1A-B2AC3CB0BF3F"),
                Category = FeedbackAnalysisPromptCategory.OpinionPointsExtraction,
            },
            new()
            {
                Id = Guid.Parse("6F1FDE2A-CAFC-4C4D-B909-655414C8C76E"),
                Category = FeedbackAnalysisPromptCategory.ActionableOpinionsAnalysis,
            },
            new()
            {
                Id = Guid.Parse("33CCCA43-E803-4FA2-AFC7-7C202DE5EA0C"),
                Category = FeedbackAnalysisPromptCategory.QuestionPointsExtraction,
            },
            new()
            {
                Id = Guid.Parse("2EF85588-0B12-4FB8-9027-80D45CC38EC1"),
                Category = FeedbackAnalysisPromptCategory.ActionableQuestionsAnalysis,
            }
        };

        await appDbContext.PromptCategories.AddRangeAsync(promptCategories);
        await appDbContext.SaveChangesAsync();
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
                Id = Guid.Parse("42204C3B-0E3E-4360-9059-94A011C29608"),
                CategoryId = Guid.Parse("7397EB27-EEAF-4898-9B0C-D78613817C30"),
                Prompt = $"""
                         ## Instructions"

                         Analyze user feedback and provide a relevance with the service in true or false format

                         Conditions :
                         1. feedback must include at least 1 sentence about the service
                         2. even feedbacks that have non-related content counts if the rule 1 is satisfied

                         ## Product Description:
                         
                         {PromptConstants.ProductDescription}
                         
                         ## Customer feedback :
                         
                         {PromptConstants.CustomerFeedback}
                         
                         ## Result

                         """,
                Version = 1,
                Revision = 0,
            },
            new()
            {
                Id = Guid.Parse("3ca01475-d736-4ac3-a326-a2580110ee0c"),
                CategoryId = Guid.Parse("787BB696-5057-4840-9161-770AD88FFA9B"),
                Prompt = $"""
                            ## Instructions"

                            Extract only relevant parts of the customer feedback for the product

                            Requirements :
                            1. if feedback contains relevant content in different parts of the feedback, all relevant parts must be extracted and appended
                            2. try to extract as a readable sentence, not just words

                            ## Product Description:
                            
                            {PromptConstants.ProductDescription}
                            
                            ## Customer feedback :
                            
                            {PromptConstants.CustomerFeedback}

                            ## Result

                            """,
                Version = 2,
                Revision = 0,
            },
            new()
            {
                Id = Guid.Parse("4ca01475-d036-4ac3-a326-a2580110ee0c"),
                CategoryId = Guid.Parse("D187624D-8AF7-4495-BF7B-00084A63372E"),
                Prompt = $"""
                            ## Instructions"

                            Redact personal information from the customer feedback

                            Requirements :
                            1. redact only words that is considered as personal information, not the whole sentence
                            2. replace the redacted words with asterisks
                            3. make sure sentences are still readable

                            ## Product Description:
                            
                            {PromptConstants.ProductDescription}
                            
                            ## Customer feedback :
                            
                            {PromptConstants.CustomerFeedback}

                            ## Result

                            """,
                Version = 3,
                Revision = 0,
            },
            new()
            {
                Id = Guid.Parse("2ba01475-d636-4ac3-a326-a2580112ee0c"),
                CategoryId = Guid.Parse("28C2137D-E6F7-440D-9513-1EE2E0B36530"),
                Prompt = $"""
                            ## Instructions"

                            Recognize languages from the customer feedback

                            Requirements :
                            1. list language if something readable or like a sentence written in it, not just a word
                            2. list all languages feedback contains multiple languages
                            3. return languages as array of strings in JSON format to deserialize 

                            ## Product Description:
                            
                            {PromptConstants.ProductDescription}
                            
                            ## Customer feedback :
                            
                            {PromptConstants.CustomerFeedback}

                            ## Result

                            """,
                Version = 4,
                Revision = 0,
            },
            new()
            {
                Id = Guid.Parse("551d1c24-24c2-45aa-9eba-383de543b24b"),
                CategoryId = Guid.Parse("B12F3C18-2706-42BB-BF1A-B2AC3CB0BF3F"),
                Prompt = $"""
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
                            
                            {PromptConstants.ProductDescription}
                            
                            ## Customer feedback :
                            
                            {PromptConstants.CustomerFeedback}

                            ## Result

                            """,
                Version = 5,
                Revision = 0,
            }
        };

        await appDbContext.Prompts.AddRangeAsync(analysisPrompts);

        var categories = appDbContext.PromptCategories.ToList();
        categories.ForEach(
            category =>
            {
                var prompt = analysisPrompts.FirstOrDefault(prompt => prompt.CategoryId == category.Id);

                if (prompt is not null)
                    category.SelectedPromptId = prompt.Id;
            }
        );

        appDbContext.PromptCategories.UpdateRange(categories);
    }

    private static async ValueTask SeedAnalysisWorkflows(AppDbContext appDbContext)
    {
        // Add template workflow execution options
        var executionOptions = new List<WorkflowPromptCategoryExecutionOptions>
        {
            new()
            {
                AnalysisPromptCategoryId = Guid.Parse("15072FC8-63C7-49EC-BF4F-3FD2A8479CF4"),
                ChildExecutionOptions =
                [
                    new WorkflowPromptCategoryExecutionOptions
                    {
                        AnalysisPromptCategoryId = Guid.Parse("7397EB27-EEAF-4898-9B0C-D78613817C30"),
                        ChildExecutionOptions =
                        [
                            new WorkflowPromptCategoryExecutionOptions
                            {
                                AnalysisPromptCategoryId = Guid.Parse("28C2137D-E6F7-440D-9513-1EE2E0B36530"),
                            },
                            new WorkflowPromptCategoryExecutionOptions
                            {
                                AnalysisPromptCategoryId = Guid.Parse("787BB696-5057-4840-9161-770AD88FFA9B"),
                                ChildExecutionOptions =
                                [
                                    new WorkflowPromptCategoryExecutionOptions
                                    {
                                        AnalysisPromptCategoryId = Guid.Parse("FD49A0B2-403F-491F-A4C4-1C489758FB79"),
                                        ChildExecutionOptions =
                                        [
                                            new WorkflowPromptCategoryExecutionOptions
                                            {
                                                AnalysisPromptCategoryId = Guid.Parse("D187624D-8AF7-4495-BF7B-00084A63372E"),
                                            },
                                            new WorkflowPromptCategoryExecutionOptions
                                            {
                                                AnalysisPromptCategoryId = Guid.Parse("B12F3C18-2706-42BB-BF1A-B2AC3CB0BF3F"),
                                                ChildExecutionOptions =
                                                [
                                                    new WorkflowPromptCategoryExecutionOptions
                                                    {
                                                        AnalysisPromptCategoryId = Guid.Parse("6F1FDE2A-CAFC-4C4D-B909-655414C8C76E"),
                                                    },
                                                ]
                                            },
                                            new WorkflowPromptCategoryExecutionOptions
                                            {
                                                AnalysisPromptCategoryId = Guid.Parse("33CCCA43-E803-4FA2-AFC7-7C202DE5EA0C"),
                                                ChildExecutionOptions =
                                                [
                                                    new WorkflowPromptCategoryExecutionOptions
                                                    {
                                                        AnalysisPromptCategoryId = Guid.Parse("2EF85588-0B12-4FB8-9027-80D45CC38EC1"),
                                                    },
                                                ]
                                            },
                                        ]
                                    },
                                ]
                            },
                            new WorkflowPromptCategoryExecutionOptions
                            {
                                AnalysisPromptCategoryId = Guid.Parse("159D0655-40AE-4DED-8C83-0FFFF69A7704"),
                            },
                        ]
                    }
                ]
            }
        };

        // Add template workflow
        var templateWorkflow = new FeedbackExecutionWorkflow
        {
            Name = "Base Workflow",
            ProductId = Guid.Parse("46E96B3C-4028-4FD5-B38A-981237BD6F9D"),
            Type = WorkflowType.Template,
            StartingExecutionOption = executionOptions.First()
        };

        appDbContext.FeedbackExecutionWorkflows.Add(templateWorkflow);

        var trainingWorkflow = templateWorkflow.Clone();
        trainingWorkflow.Name = "Training Workflow";
        trainingWorkflow.Type = WorkflowType.Training;
        trainingWorkflow.ProductId = Guid.Parse("46E96B3C-4028-4FD5-B38A-981237BD6F9D");

        appDbContext.FeedbackExecutionWorkflows.Add(trainingWorkflow);

        await appDbContext.SaveChangesAsync();
    }
}