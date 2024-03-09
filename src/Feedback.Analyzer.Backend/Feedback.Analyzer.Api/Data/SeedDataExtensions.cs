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
                Comment =
                    "I laid my hands on the Viper in a local store and on the spot it felt rather flat. I think I prefer something with more hump and side area to grip onto. right now I have a basilisk v2 and think it's definetely more comfy, but that grip could still be better.\n\nThe Synapse software is extremely greedy though. 300MB HD space and about 200MB RAM is ludicrous for a gloryfied mouse driver.",
                UserName = "Silvarspark"
            },
            new()
            {
                ProductId = Guid.Parse("46E96B3C-4028-4FD5-B38A-981237BD6F9D"),
                Comment =
                    "I would buy the product if the price was a bit lower. One can find many good options in this price range. I would rather buy a brand new RGB keyboard if I had such money. Overall the product is not bad but it is overpriced.",
                UserName = "John Doe"
            },
            new()
            {
                ProductId = Guid.Parse("46E96B3C-4028-4FD5-B38A-981237BD6F9D"),
                Comment = "Can anyone help me decide which mouse is the best one on the market now? I used Viper before but not satisfied.",
                UserName = "John Smith"
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
                Prompt = """
                         ### Product Description
                         {{$productDescription}}

                         ### Customer Feedback
                         {{$customerFeedback}}

                         ### Instructions
                         Decide if the given Customer Feedback (delimited by ###) is relevant or irrelevant to the given Product Description (delimited by ###).
                         Relevant - "true"
                         irrelevant - "false"

                         Conditions:
                         1. Even the slightest mention of the product, its features, name or service in the Customer Feedback is counted as relevant.
                         2. Questions about product, its name and features, or service is also considered as relevant.
                         3. Any word or expression that is used to imply the product, its name or features, or service is considered as relevant.
                         4. Any word or expression indicating an issue with the product or service, its functionality.

                         The return format: "true" or "false"

                         """,
                Version = 1,
                Revision = 0,
            },
            new()
            {
                Id = Guid.Parse("3ca01475-d736-4ac3-a326-a2580110ee0c"),
                CategoryId = Guid.Parse("787BB696-5057-4840-9161-770AD88FFA9B"),
                Prompt = """
                         ## Product Description
                         {{$productDescription}}

                         ## Customer Feedback
                         {{$customerFeedback}}

                         ## Instructions

                         Exclude the parts of the Customer Feedback that are irrelevant to the Product. Return the Customer Feedback after exclusion of irrelevant parts in a plain text.
                         Example Result: "..."
                         """,
                Version = 2,
                Revision = 0,
            },
            new()
            {
                Id = Guid.Parse("4ca01475-d036-4ac3-a326-a2580110ee0c"),
                CategoryId = Guid.Parse("FD49A0B2-403F-491F-A4C4-1C489758FB79"),
                Prompt = """
                         ## Instructions"

                         Redact personal information from the customer feedback

                         Requirements :
                         1. redact only words that is considered as personal information, not the whole sentence
                         2. replace the redacted words with asterisks
                         3. make sure sentences are still readable

                         ## Product Description:


                         {{$productDescription}}

                         ## Customer feedback :

                         {{$customerFeedback}}

                         ## Result

                         """,
                Version = 3,
                Revision = 0,
            },
            new()
            {
                Id = Guid.Parse("2ba01475-d636-4ac3-a326-a2580112ee0c"),
                CategoryId = Guid.Parse("28C2137D-E6F7-440D-9513-1EE2E0B36530"),
                Prompt = """
                         ## Instructions"

                         Recognize languages from the customer feedback

                         Requirements :
                         1. list language if something readable or like a sentence written in it, not just a word
                         2. list all languages feedback contains multiple languages
                         3. return languages as array of strings in JSON format to deserialize

                         ## Product Description:

                         {{$productDescription}}

                         ## Customer feedback :

                         {{$customerFeedback}}

                         ## Result

                         """,
                Version = 4,
                Revision = 0,
            },
            new()
            {
                Id = Guid.Parse("551d1c24-24c2-45aa-9eba-383de543b24b"),
                CategoryId = Guid.Parse("B12F3C18-2706-42BB-BF1A-B2AC3CB0BF3F"),
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
                Version = 5,
                Revision = 0,
            },
            new()
            {
                Id = Guid.Parse("6d4b569a-2df7-49ae-9d8a-7b6fdcc8f3af"),
                CategoryId = Guid.Parse("159D0655-40AE-4DED-8C83-0FFFF69A7704"),
                Prompt = """
                         ##Entity Identification from User Feedback:
                         Objective: Analyze user feedback to extract and identify key entities, organizing these into clear key phrases paired with corresponding values to enhance understanding and facilitate actionable responses.

                         ##Instructions:

                         Extract Key Phrases:

                         Identify Entities: Locate significant nouns or phrases in the feedback that symbolize distinct entities, like product features, issues, or user sentiments.

                         Key Phrases Extraction: Catalog these entities as key phrases that reflect the primary subjects of the feedback.

                         Assign Key-Value Pairs:
                         For each identified key phrase, assign a value that aptly represents its sentiment, frequency, or importance as conveyed in the feedback.

                         Organize Information:
                         Arrange the extracted information methodically, with emphasis on relevance or urgency as dictated by the feedback's context.

                         Context Provided:

                         Product Description: {{$productDescription}}
                         Provides background to help discern which feedback aspects are pertinent to the product features or services.

                         User Feedback: {{$userFeedback}}
                         The actual feedback from users that will be analyzed to identify and categorize key entities.

                         ##Expected Output:
                         A structured set of key phrases derived from the user feedback.
                         Corresponding key-value pairs for each identified phrase, supplying additional detail or contextual information.

                         ##Result:
                         Key Phrases with Key-Value Pairs:

                         This should return a dictionary-like structure (in a format appropriate for display) where each entry consists of a 'Phrase' (key) and its associated 'Value' (sentiment, frequency, importance). For example:
                            
                            Phrase: "Battery Life"; Value: "Short - Negative"
                            Phrase: "Customer Service"; Value: "Helpful - Positive"
                            
                         """,
                Version = 6,
                Revision = 0,
            },
            new()
            {
                Id = Guid.Parse("82e41f2b-017d-46b4-95bf-17cfb9e31be6"),
                CategoryId = Guid.Parse("33CCCA43-E803-4FA2-AFC7-7C202DE5EA0C"),
                Prompt = """
                         ##Question Points Extraction from User Feedback:
                         Objective: Sift through user feedback to detect and compile a comprehensive list of questions or areas where clarification is sought, aiming for better engagement and resolution.

                         ##Instructions:

                         -Identify Questions:

                         -Review Feedback: Diligently go through the feedback provided by users.

                         -Extract Questions: Identify parts of the feedback where questions are posed or clarification is requested. These are typically interrogative statements or sentences ending with a question mark.

                         -Compile Questions:

                         List all questions identified from the feedback, making sure they are clear and retain the necessary context to be understood independently of the feedback.

                         -Categorize Questions:
                         If possible, organize the questions into categories based on common themes, such as Product Features, Usage, Support, etc.

                         -Context Provided:
                         Product Description: {{$productDescription}}

                         Understanding the product or service in question to better frame the context of the feedback and the nature of the questions posed.
                         User Feedback: {{$userFeedback}}

                         Direct feedback from users, which will be analyzed to extract relevant questions.
                         Expected Output:
                         Collection of Questions: Enumerate the questions that have been extracted from the user feedback. These questions should be presented clearly and concisely, capturing the users' inquiries or points of confusion.
                         ##Result:

                         [Collection of Questions]: This should return an array or collection of strings, each string being a question extracted from the user feedback. Each question should maintain its context to ensure that what is being asked is understandable.
                         Example output based on your structure could be:
                         
                             "How long does the battery last on a single charge?"
                             "Can the software be updated to fix the current lag issues?"

                         """,
                Version = 7,
                Revision = 0,
            },
            new()
            {
                Id = Guid.Parse("f2d1c8ac-afe7-42d8-b277-7f8c5243e38f"),
                CategoryId = Guid.Parse("D187624D-8AF7-4495-BF7B-00084A63372E"),
                Prompt = """
                         ##Advanced Opinion Mining
                         Objective: Evaluate the sentiment of user feedback in relation to the detailed product description provided. The analysis should discern nuanced language and contextual clues tied to specific attributes of the product mentioned within the feedback.

                         Context Provided:
                         Product Description: {productDescription}

                         Utilize this information to understand the context of the feedback and how specific product features are being addressed by the users.
                         User Feedback: {userFeedback}

                         This is the actual feedback provided by the user that needs to be analyzed for sentiment.
                         Sentiment Classification Criteria:
                         Positive: The feedback should be considered positive if it expresses satisfaction, praises product features, or otherwise indicates a good user experience.

                         Negative: The feedback is negative if it includes complaints, dissatisfaction, issues, or problems related to the product.

                         Neutral: Classify the feedback as neutral if it neither explicitly expresses satisfaction nor dissatisfaction, or if it simply provides factual information without any emotional sentiment.

                         Instruction for Analysis:
                         Analyze the feedback carefully, taking into account the product description and the specific aspects mentioned in the feedback.
                         Consider the overall tone, choice of words, and the context in which product features are discussed.
                         Classify the overall sentiment of the feedback based on the criteria outlined above.

                         Expected Outcome:
                         ##Result: The sentiment of the user feedback classified into one of the following enums: Positive, Negative, Neutral.

                         Ensure your response is concise and directly correlates to the sentiments expressed in the user feedback, reflecting a clear understanding of the user's perspective in relation to the product described.
                         """,
                Version = 8,
                Revision = 0,
            },
            new()
            {
                Id = Guid.Parse("9f5f11d2-33f4-47bf-b7dd-29868df7e167"),
                CategoryId = Guid.Parse("6F1FDE2A-CAFC-4C4D-B909-655414C8C76E"),
                Prompt = """
                         ##Detailed Feedback Analysis for Actionable Insights:
                         Objective: Evaluate user comments to deduce their overarching messages and categorize the feedback based on its nature and potential for action.

                         ##Instructions:

                         -Summarize and Interpret Feedback:
                         -Review User Comment: Thoroughly examine the provided user feedback.
                         -Draft Overall Summary: Create a brief summary that encapsulates the main message or sentiment conveyed in the feedback.
                         -Evaluate and Classify Feedback:
                         -Assess Nature of Feedback: Ascertain whether the feedback is 'generic' (relating to broad concerns or opinions) or 'specific' (pertaining to concrete, detailed observations).
                         -Determine Actionability: Decide if the feedback contains actionable suggestions or improvements.
                         -Document Analysis and Recommendations:
                         Offer recommendations or identify actions based on the feedback's content and nature, considering both immediate and long-term improvements.

                         Context Provided:
                         ##Product Description: {productDescription}

                         ##User Feedback: {userFeedback}

                         ##Expected Output:
                         Feedback Summary: Provide a succinct interpretation that reflects the main message of the user's feedback.

                         ##Classification:
                         Nature: Label the feedback as either 'Generic' or 'Specific'.
                         Actionability: Classify the feedback as 'Actionable' or 'Non-Actionable'.

                         ##Result:
                         [Detailed Feedback Analysis]: Return a string array containing the classifications for each piece of user feedback analyzed. The array should include labels such as 'General', 'Specific', 'Actionable', and 'Nonactionable', based on the analysis conducted according to the provided instructions.

                         """,
                Version = 9,
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
        var executionOptions = new List<WorkflowExecutionOption>
        {
            // new()
            // {
            //     AnalysisPromptCategoryId = Guid.Parse("15072FC8-63C7-49EC-BF4F-3FD2A8479CF4"),
            //     ChildExecutionOptions =
            //     [
            new WorkflowExecutionOption
            {
                AnalysisPromptCategoryId = Guid.Parse("7397EB27-EEAF-4898-9B0C-D78613817C30"),
                ChildExecutionOptions =
                [
                    // new WorkflowExecutionOption
                    // {
                    //     AnalysisPromptCategoryId = Guid.Parse("28C2137D-E6F7-440D-9513-1EE2E0B36530"),
                    // },
                    new WorkflowExecutionOption
                    {
                        AnalysisPromptCategoryId = Guid.Parse("787BB696-5057-4840-9161-770AD88FFA9B"),
                        ChildExecutionOptions =
                        [
                            new WorkflowExecutionOption
                            {
                                AnalysisPromptCategoryId = Guid.Parse("FD49A0B2-403F-491F-A4C4-1C489758FB79"),
                                ChildExecutionOptions =
                                [
                                    // new WorkflowExecutionOption
                                    // {
                                    //     AnalysisPromptCategoryId = Guid.Parse("D187624D-8AF7-4495-BF7B-00084A63372E"),
                                    // },
                                    // new WorkflowExecutionOption
                                    // {
                                    //     AnalysisPromptCategoryId = Guid.Parse("B12F3C18-2706-42BB-BF1A-B2AC3CB0BF3F"),
                                    //     ChildExecutionOptions =
                                    //     [
                                    //         new WorkflowExecutionOption
                                    //         {
                                    //             AnalysisPromptCategoryId = Guid.Parse("6F1FDE2A-CAFC-4C4D-B909-655414C8C76E"),
                                    //         },
                                    //     ]
                                    // },
                                    // new WorkflowExecutionOption
                                    // {
                                    //     AnalysisPromptCategoryId = Guid.Parse("33CCCA43-E803-4FA2-AFC7-7C202DE5EA0C"),
                                    //     ChildExecutionOptions =
                                    //     [
                                    //         new WorkflowExecutionOption
                                    //         {
                                    //             AnalysisPromptCategoryId = Guid.Parse("2EF85588-0B12-4FB8-9027-80D45CC38EC1"),
                                    //         },
                                    //     ]
                                    // },
                                ]
                            },
                        ]
                    },
                    // new WorkflowExecutionOption
                    // {
                    //     AnalysisPromptCategoryId = Guid.Parse("159D0655-40AE-4DED-8C83-0FFFF69A7704"),
                    // },
                ]
            }
            //         ]
            //     }
        };

        // Add template workflow
        var analysisWorkflow = new AnalysisWorkflow
        {
            Id = Guid.Parse("7D5A3D3E-1DC7-4365-A371-CB55C83938CA"),
            Name = "Base Workflow",
            Type = WorkflowType.Training,
            EntryExecutionOption = executionOptions.First()
        };

        appDbContext.AnalysisWorkflows.Add(analysisWorkflow);

        var feedbackAnalysisWorkflow = new FeedbackAnalysisWorkflow
        {
            Id = Guid.Parse("7D5A3D3E-1DC7-4365-A371-CB55C83938CA"),
            ProductId = Guid.Parse("46E96B3C-4028-4FD5-B38A-981237BD6F9D"),
        };

        appDbContext.FeedbackExecutionWorkflows.Add(feedbackAnalysisWorkflow);

        // var trainingWorkflow = templateWorkflow.Clone();
        // trainingWorkflow.Name = "Training Workflow";
        // trainingWorkflow.Type = WorkflowType.Training;
        // trainingWorkflow.ProductId = Guid.Parse("46E96B3C-4028-4FD5-B38A-981237BD6F9D");

        // appDbContext.FeedbackExecutionWorkflows.Add(trainingWorkflow);

        await appDbContext.SaveChangesAsync();
    }
}