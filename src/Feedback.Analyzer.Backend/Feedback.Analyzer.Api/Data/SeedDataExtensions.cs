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
            await SeedCustomerFeedbacksAsync(appDbContext);

        if (!await appDbContext.PromptCategories.AnyAsync())
        {
            await SeedPromptCategoriesAsync(appDbContext);
            await SeedAnalysisPromptAsync(appDbContext);
        }

        if (!await appDbContext.WorkflowExecutionOptions.AnyAsync())
            await SeedAnalysisExecutionOptions(appDbContext);

        if (!await appDbContext.AnalysisWorkflows.AnyAsync())
            await SeedAnalysisWorkflowsAsync(appDbContext);

        if (!await appDbContext.FeedbackAnalysisWorkflows.AnyAsync())
            await SeedFeedbackAnalysisWorkflowsAsync(appDbContext);

        if (appDbContext.ChangeTracker.HasChanges())
            await appDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Seeds client data asynchronously.
    /// </summary>
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
                PasswordHash = "$2a$12$pHdneNbJGp4SnN1ovHrNqevf6I.k3Gy.7OMJoWWB0RByv0foi4fgy", // qwerty123
                Role = Role.Admin
            },
            new()
            {
                Id = Guid.Parse("5edbb0fe-7263-4f75-bad8-c9f3d422dd1d"),
                FirstName = "Sarah",
                LastName = "Funk",
                EmailAddress = "sarah.funk@gmail.com",
                PasswordHash = "$2a$12$LxSqe5AE7AtglesHHK5NROFdJQdA1r1XKqhzg4q/tMTZjVEH0PNSK", //asdf1234
                Role = Role.Client
            }
        };

        await dbContext.Clients.AddRangeAsync(clients);
    }

    /// <summary>
    /// Seeds organization data asynchronously.
    /// </summary>
    private static async ValueTask SeedOrganizationAsync(AppDbContext appDbContext)
    {
        var organizations = new List<Organization>
        {
            new()
            {
                Id = Guid.Parse("c2fe1019-1180-4f3e-b477-413a9b33bbd1"),
                Name = "Razer Inc.",
                Description = """
                              What is Razer?

                              Razer is a global technology company at the forefront of the gaming industry. It specializes in designing and
                              manufacturing cutting-edge hardware, software, and services with a passionate focus on the gaming and esports
                              communities. Razer's iconic triple-headed snake logo is widely recognized as a symbol of excellence and innovation in
                              the gaming world.

                              Little History

                              1998: The Razer brand is born in San Diego, California, under a small company named kärna.
                              2005: Razer Inc. is officially founded by Min-Liang Tan (current CEO) and Robert Krakoff.
                              Through the years: Razer establishes itself as a powerhouse through strategic product releases and acquisitions.
                              CEO, Managers, Main Roles

                              CEO: Min-Liang Tan (Co-founder)
                              CFO: Chong Neng Tan
                              President: Richard Hashim
                              Key Roles: Hardware engineers, software developers, product designers, marketing specialists, and esports/community
                              management. What Products the Company Manufactures

                              Gaming Laptops: High-performance gaming laptops under the Razer Blade series, known for slim designs and powerful
                              components. Gaming Mice: A wide variety of gaming mice catering to different grip styles, sensor technologies, and
                              levels of customization. Gaming Keyboards: Mechanical and membrane keyboards with features like RGB lighting,
                              programmable macros, and dedicated media controls. Audio: Gaming headsets, speakers, and microphones optimized for
                              immersive sound and clear communication. Accessories: Mousepads, gaming chairs, backpacks, and other peripherals to
                              complement the gaming experience. Software & Services: Razer Synapse (device configuration), Razer Chroma RGB
                              (lighting ecosystem), Razer Cortex (game optimization), Razer Gold (virtual currency)
                              """,
                ClientId = Guid.Parse("54e16318-d140-4453-80c9-1a117dbe75fd"),
            },
        };

        await appDbContext.Organizations.AddRangeAsync(organizations);
        await appDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Seeds product data asynchronously.
    /// </summary>
    private static async ValueTask SeedProductsAsync(AppDbContext appDbContext)
    {
        var products = new List<Product>
        {
            new()
            {
                Id = Guid.Parse("751d1c24-24c2-45aa-9eba-383de543b34b"),
                OrganizationId = Guid.Parse("c2fe1019-1180-4f3e-b477-413a9b33bbd1"),
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
                              Fully configurable via Razer Synapse 3, the 8 programmable buttons let you access macros and secondary functions so you can execute extended moves with ease.
                              """
            }
        };

        await appDbContext.Products.AddRangeAsync(products);
    }

    /// <summary>
    /// Seeds customer feedbacks
    /// </summary>
    private static async ValueTask SeedCustomerFeedbacksAsync(AppDbContext appDbContext)
    {
        var customersFeedbacks = new List<CustomerFeedback>()
        {
            // Positive feedback
            new()
            {
                ProductId = Guid.Parse("751d1c24-24c2-45aa-9eba-383de543b34b"),
                Comment = """
                          I laid my hands on the Viper in a local store and on the spot it felt rather flat. I think I prefer something with more
                          hump and side area to grip onto. right now I have a basilisk v2 and think it's definetely more comfy, but that grip could
                          still be better.\n\nThe Synapse software is extremely greedy though. 300MB HD space and about 200MB RAM is ludicrous for a
                          gloryfied mouse driver.",
                          """,
                UserName = "Silverspark",
            },
        };

        await appDbContext.Feedbacks.AddRangeAsync(customersFeedbacks);
    }

    /// <summary>
    /// Seeds prompt categories
    /// </summary>
    private static async ValueTask SeedPromptCategoriesAsync(AppDbContext appDbContext)
    {
        var promptCategories = new List<AnalysisPromptCategory>
        {
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
    /// Seeds prompts
    /// </summary>
    private static async ValueTask SeedAnalysisPromptAsync(AppDbContext appDbContext)
    {
        var analysisPrompts = new List<AnalysisPrompt>()
        {
            new()
            {
                Id = Guid.Parse("42204C3B-0E3E-4360-9059-94A011C29608"),
                CategoryId = Guid.Parse("7397EB27-EEAF-4898-9B0C-D78613817C30"),
                Prompt = """
                         ## Concepts

                         Product description : A brief description of the product
                         Customer feedback : The customer's feedback comment about the product
                         Relevance : measure of how closely the customer's feedback is related to the product description

                         ## Instructions

                         Analyze customer feedback, compare it with product description and determine whether the customer's feedback is relevant or
                         not. Deeply analyze the description to understand the customer's feedback.

                         Requirements :
                         1. feedback must include at least 1 sentence about the service
                         2. even feedbacks that have non-related content counts if the rule #1 is satisfied
                         3. output a single line in the following format

                         true - if feedback is relevant
                         false - if feedback is not relevant

                         ## Examples

                         Product description :

                         Logitech MX Master 3 Wireless Mouse. The most advanced Master Series mouse yet – designed for creatives and engineered for
                         coders. If you can think it, you can master it. The great battery life and the ergonomic design make it a perfect choice for
                         all day use.

                         1. Relevant feedback

                         Customer feedback : battery life ? are you joking ? mine lasts only 2 days. I'm really disappointed with the product.
                         Reasoning : Here customer is talking about battery life of the product which is relevant to the product, result is true

                         2. Non relevant feedback

                         Customer feedback : I like keyboards of this brand. They are really good.
                         Reasoning : Even though feedback mentions the brand, it's not directly has anything to do with the product. Customer is
                                  talking about the different product of the brand, so result is false

                         3. Advanced case

                         Customer feedback : finally a good one
                         Reasoning : Customer is talking about the product in a positive way but not mentioning any specific feature of the
                         product, result is true

                         ## Input

                         Product description - {{$productDescription}}
                         Customer feedback - {{$customerFeedback}}
                         """,
                Version = 1,
                Revision = 0,
            },
            new()
            {
                Id = Guid.Parse("3ca01475-d736-4ac3-a326-a2580110ee0c"),
                CategoryId = Guid.Parse("787BB696-5057-4840-9161-770AD88FFA9B"),
                Prompt = """
                         ## Concepts

                         Product description : A brief description of the product
                         Customer feedback : The customer's feedback comment about the product
                         Relevant content : Only relevant part of the customer's feedback

                         ## Instructions

                         Analyze customer feedback, compare it with product description and extract only parts of the feedback that is relevant to the product or can contribute to the product improvement. Deeply analyze the description to understand the customer's feedback.

                         Requirements :
                         1. if relevant content is in different parts of the feedback, all relevant parts must be extracted and appended
                         2. try to extract as a readable sentence, not just words
                         3. output the result as a string

                         ## Examples

                         Product description :

                         Logitech MX Master 3 Wireless Mouse. The most advanced Master Series mouse yet – designed for creatives and engineered for
                         coders. If you can think it, you can master it. The great battery life and the ergonomic design make it a perfect choice for
                         all day use.

                         1. Relevant content in different parts

                         Customer feedback : battery life ? are you joking ? mine lasts only 2 days. I'm really disappointed with the product. last
                         year I bought a Rapoo MT550 mouse, and it still has a great battery life. Battery life in Razer mouses just sucks

                         Result : battery life ? are you joking ? mine lasts only 2 days. I'm really disappointed with the product.
                         Battery life in Razer mouses just sucks

                         Reasoning : Feedback has 3 parts - complaint about battery life, mentioning other brand's product and another complaint
                         about the product. We extracted only part 1 and part 3, as mentioning other brand's product is not relevant

                         2. Relevant content in a single part

                         Customer feedback : I like keyboards of this brand. They are really good. I hope the mouse is as good as the keyboards

                         Result : I hope the mouse is as good as the keyboards

                         Reasoning : Feedback has 3 parts - compliment about keyboards and hoping that mouses are as good as keyboards. We extract
                         the part 2, although it skips the initial context, it gives clear view of the customer's expectation

                         3. Advanced case

                         Customer feedback : Got one month ago, it feels very big and not comfy, my fist hurts when using it

                         Result : Got one month ago, it feels very big and not comfy, my fist hurts when using it

                         Reasoning : Feedback has 3 parts - customer mentions when he bought the product, how it feels and the problem he is facing.
                         Whole feedback is relevant

                         ## Input

                         Product description - {{$productDescription}}
                         Customer feedback - {{$customerFeedback}}
                         """,
                Version = 1,
                Revision = 0,
            },
            new()
            {
                Id = Guid.Parse("4ca01475-d036-4ac3-a326-a2580110ee0c"),
                CategoryId = Guid.Parse("FD49A0B2-403F-491F-A4C4-1C489758FB79"),
                Prompt = """
                         ## Instructions

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
                Version = 1,
                Revision = 0,
            },
            new()
            {
                Id = Guid.Parse("2ba01475-d636-4ac3-a326-a2580112ee0c"),
                CategoryId = Guid.Parse("28C2137D-E6F7-440D-9513-1EE2E0B36530"),
                Prompt = """
                         ## Concepts
                         
                         Customer feedback : The customer's feedback comment about the product
                         Language Recognition : Recognizing which languages are used in the feedback
                         
                         ## Instructions
                         
                         Analyze customer feedback, understand the language used in the feedback and recognize the languages used in the feedback.
                         
                         Requirements :
                         1. recognize all languages used in the feedback
                         2. output the result as array of strings, don't use fenced code blocks for JSON, just output as JSON as following : 
                         
                         [
                            "English",
                            "Spanish"
                         ]
                         
                         ## Examples
                         
                         1. Single language
                         
                         Customer Feedback : I've been using the Logitech MX Master 3 for a couple of months now, and I'm extremely satisfied. The 
                         ergonomic design has significantly reduced my wrist pain, and the battery life is just incredible. The customizable buttons 
                         have streamlined my workflow remarkably.
                         Result : [ "English" ]
                         Reasoning : Customer feedback contains only content in english language
                         
                         2. Multiple languages
                         
                         Customer Feedback : I absolutely love my Logitech MX Master 3! The smooth scrolling feature and the precise tracking have 
                         improved my editing work tremendously. However, tengo un pequeño problema con el software de personalización; a veces no 
                         guarda mis configuraciones correctamente. But overall, it's a fantastic product. 
                         Result : [ "English", "Spanish" ]
                         Reasoning : 1st part of the customer feedback is in english and the second part is in spanish
                         
                         3. Advanced case
                         
                         Customer Feedback :The Logitech MX Master 3 is a game-changer for me. The comfort and the advanced features are top-notch. 
                         However, Ich hatte Probleme mit der Verbindung via Bluetooth, which was a bit frustrating. Also, la qualité de construction
                         est exceptionnelle, making it worth every penny despite the minor issues.
                         Result : [ "English", "French" ]
                         Reasoning : Although customer feedback mainly in english language, in the middle of the feedback, there is a sentence in 
                         franch
                         
                         ## Input
                         
                         Customer feedback - {{$customerFeedback}}
                         """,
                Version = 1,
                Revision = 0,
            },
            new()
            {
                Id = Guid.Parse("551d1c24-24c2-45aa-9eba-383de543b24b"),
                CategoryId = Guid.Parse("B12F3C18-2706-42BB-BF1A-B2AC3CB0BF3F"),
                Prompt = """
                         ## Concepts

                         Product description : A brief description of the product
                         Customer feedback : The customer's feedback comment about the product
                         Positive point : One unit of positive opinion from the customer's feedback
                         Negative point : One unit of negative opinion point from the customer's feedback

                         ## Instructions

                         Analyze customer feedback, compare it with product description and extract positive and negative points from the feedback.
                         Deeply analyze the description to understand the customer's feedback.

                         Requirements :
                         1. something counts as positive point if it is a compliment about some exact thing in the product, not just customer happiness
                            expression
                         2. something counts as negative point if it is a complaint about some exact thing in the product, not just customer disappointment
                            expression
                         3. don't include neutral points or something general like "I like the product" or "I hate the product"
                         4. you can rephrase the points to make them more readable and simpler
                         5. if a sentence has multiple contains multiple points, extract and separate them all
                         6. output must be an array of an array, first array contains positive points, second array contains negative points, if any
                         of points not found for the category just return empty array in JSON format as following, don't use fenced code blocks for
                         JSON

                         [
                            [
                                "I like the side grip of the mouse",
                                "battery life has improved"
                            ],
                            [
                                "Mouse driver is hard to update",
                                "When battery is low, mouse starts lagging",
                            ]
                         ]

                         ## Examples

                         Product description :

                         Logitech MX Master 3 Wireless Mouse. The most advanced Master Series mouse yet – designed for creatives and engineered for
                         coders. If you can think it, you can master it. The great battery life and the ergonomic design make it a perfect choice for
                         all day use.

                         1. Relevant content in different parts

                         Customer feedback : battery life ? are you joking ? mine lasts only 2 days. I'm really disappointed with the product. last
                         year I bought a Rapoo MT550 mouse, and it still has a great battery life. I hate Razer mouses

                         Result :

                         [
                            [],
                            [
                                "battery doesn't last long enough",
                            ]
                         ]

                         Reasoning : Customer has exact complaint about the battery and expresses that he hates Razer mouses, here he said that
                         battery life lasts for only 2 days, which is a negative point, but the last expression doesn't count, as it doesn't have
                         some exact complaint about the product

                         2. Relevant content in a single part

                         Customer feedback : The DPI in the new mouse just rocks, it's perfect for design work,  but the polling rate isn't grate for
                         gaming

                         Result :

                         [
                            [
                                "improved DPI since last model",
                            ],
                            [
                                "bad polling rate for gaming"
                            ]
                         ]

                         Reasoning : Customer has exact compliment about the DPI and exact complaint about the polling rate, so both are extracted

                         3. Advanced case

                         Product description : My friend recommended the mouse as it has a good buttons layout and also mentioned about the scroll
                         that often breaks. I bought one, so far good, just over time, side buttons started to get stuck and hard to click, charging
                         in this mouse is pretty quick tho

                         Result :

                         [
                            [
                                "quick charging",
                            ],
                            [
                                "side buttons are getting stuck",
                            ]
                         ]

                         Reasoning : Although customer has mentioned the experience of hist friend, this doesn't count as direct experience with the
                         product, so we can only take the last 2 points

                         ## Input

                         Product description - {{$productDescription}}
                         Customer feedback - {{$customerFeedback}}
                         """,
                Version = 1,
                Revision = 0,
            },
            new()
            {
                Id = Guid.Parse("d2fe6123-dc50-4b4a-93a0-11f2263aa09c"),
                CategoryId = Guid.Parse("159d0655-40ae-4ded-8c83-0ffff69a7704"),
                Prompt = """
                            ##Entity Identification from User Feedback:
                            ##Objective: Extract and identify key entities from user feedback, organizing them into key phrases with associated key-value pairs for clearer understanding and action.
                                
                            ##Instructions:
                            ##Extract Key Phrases:
                                 
                             Identify Entities: Pinpoint significant nouns or phrases from the feedback that represent distinct entities, such as product features, issues, or user emotions.
                                 Key Phrases Extraction: List these as key phrases indicative of the feedback's main topics.
                             ##Assign Key-Value Pairs:
                                 
                                 For each key phrase, determine and assign a value that best represents the sentiment, frequency, or importance mentioned in the feedback.
                              ##Organize Information:
                                 
                                 Structure the extracted information into an easy-to-read format, prioritizing by relevance or urgency based on the feedback context.
                            
                             ##Product Description:
                                {{$productDescription}}
                         
                             ##User Feedback:
                                {{$userFeedback}}
                         
                             ##Expected Output:
                                 A list of key phrases extracted from the user feedback.
                                 Associated key-value pairs for each phrase, providing additional detail or context.
                            ##Result:
                              Key Phrases with Key-Value Pairs:
                               Example:
                                 Phrase: ""Battery Life""; Value: ""Short - Negative""
                                 Phrase: ""Customer Service""; Value: ""Helpful - Positive""
                         """,
                Version = 1,
                Revision = 0
            },
            new()
            {
                Id = Guid.Parse("8e1d69ab-c9e1-457f-aa93-f03dbe5f7256"),
                CategoryId = Guid.Parse("33ccca43-e803-4fa2-afc7-7c202de5ea0c"),
                Prompt = """
                             ##Question Points Extraction from User Feedback:
                             Objective: Identify and compile a list of questions or areas of uncertainty mentioned in user feedback, facilitating targeted inquiry and clarification.
                              
                          ##Instructions:
                             Identify Questions:
                              
                             Review Feedback: Thoroughly examine the user feedback.
                             Extract Questions: Look for interrogative statements or points where the user seems to seek information or clarification.
                             Compile Questions:
                              
                             List out all identified questions, ensuring they retain the context needed to understand what is being asked.
                             Categorize Questions:
                              
                             If applicable, categorize the questions based on common themes, such as product features, usage, support, etc.

                         ##Product Description:
                              {{$productDescription}}

                         ##User Feedback:
                              {{$userFeedback}}

                         ##Expected Output:
                              A collection of questions extracted from the feedback, presented clearly and concisely.
                         ##Result:
                           Collection of Questions:
                             Example:
                              ""How long does the battery last on a single charge?""
                              ""Can the software be updated to fix the current lag issues?""
                         """,
                Version = 1,
                Revision = 0
            },
            new()
            {
                Id = Guid.Parse("ad52fe38-23f4-4aa2-bf60-c8a610089a89"),
                CategoryId = Guid.Parse("d187624d-8af7-4495-bf7b-00084a63372e"),
                Prompt = """
                         ## Concepts

                         Product description : A brief description of the product
                         Customer feedback : The customer's feedback comment about the product
                         Opinion mining : Determining sentiment of the customer's feedback

                         ## Instructions

                         Analyze customer feedback, compare it with product description and understand the overall sentiment.

                         Requirements :
                         1. consider nuanced language, contextual clues, and specific product attributes mentioned in the feedback
                         2. if customer doesn't have experience with the product ( language - I tried, looked, heard, seems like ), you can count
                            this as neutral
                         3. output the result as single string in JSON format as following, don't use fenced code blocks for JSON, just add double quotes to
                         make it parseable as JSON

                         "Positive" - if overall feedback has positive sentiment
                         "Negative" - if customer is not satisfied with the product
                         "Neutral" - if feedback is neither positive nor negative

                         ## Examples

                         Product description :

                         Logitech MX Master 3 Wireless Mouse. The most advanced Master Series mouse yet – designed for creatives and engineered for
                         coders. If you can think it, you can master it. The great battery life and the ergonomic design make it a perfect choice for
                         all day use. Battery life is upto 70 days on a full charge

                         1. Negative feedback

                         Customer feedback : I only get to use it 5 days after charging, the best battery life so far ))
                         Result : Negative
                         Reasoning : Although from the tone it seems like customer liked the product, they are complaining about the battery life
                         with a sarcasm

                         2. Positive feedback

                         Customer feedback : First I hated using it, the scroll, the grip ... now loving this badboy
                         Result : Positive
                         Reasoning : Customer have been adapted to the product and now loving it

                         3. Advanced case

                         Customer feedback : Tried one in store, seems just like the previous one, just more hype
                         Result : Neutral
                         Reasoning : At first it seems like customer is complaining about less than expected changes since the last release, but
                         at the same time customer admits they don't have direct experience with the product, so it doesn't as negative or positive

                         4. With turning points

                         Customer feedback : Overall I think the MX Master 3 is a good mouse, but I can't afford one
                         Result : Neutral
                         Reasoning : We count this as neutral because user doesn't have direct experience with the product

                         Customer feedback : Overall I think the MX Master 3 is a good mouse, but not for me
                         Result : Neutral
                         Reasoning : Customer feels this mouse isn't for them, and probably they haven't tried it too, so it counts as neutral

                         Customer feedback : Overall MX Master 3 is a good mouse, they said, but nope
                         Result : Negative
                         Reasoning : Customer mentions overall positive feedback about the product, but has some negative experience with it, so it
                         counts as negative

                         ## Input

                         Product description - {{$productDescription}}
                         Customer feedback - {{$customerFeedback}}

                         """,
                Version = 1,
                Revision = 0
            }
        };

        await appDbContext.Prompts.AddRangeAsync(analysisPrompts);

        var categories = appDbContext.PromptCategories.ToList();
        categories.ForEach(
            category =>
            {
                var prompt = analysisPrompts.Find(prompt => prompt.CategoryId == category.Id);

                if (prompt is not null)
                    category.SelectedPromptId = prompt.Id;
            }
        );

        appDbContext.PromptCategories.UpdateRange(categories);
    }

    /// <summary>
    /// Seeds analysis execution options
    /// </summary>
    /// <param name="appDbContext"></param>
    private static async ValueTask SeedAnalysisExecutionOptions(AppDbContext appDbContext)
    {
        // Add template workflow execution options
        var executionOptions = new WorkflowExecutionOption
        {
            Id = Guid.Parse("E4B16AEB-41C3-4E50-AB0B-8A883AE397C1"),
            AnalysisPromptCategoryId = Guid.Parse("7397EB27-EEAF-4898-9B0C-D78613817C30"),
            ChildExecutionOptions =
            [
                new WorkflowExecutionOption
                {
                    AnalysisPromptCategoryId = Guid.Parse("28C2137D-E6F7-440D-9513-1EE2E0B36530"),
                },
                new WorkflowExecutionOption
                {
                    AnalysisPromptCategoryId = Guid.Parse("787BB696-5057-4840-9161-770AD88FFA9B"),
                    ChildExecutionOptions =
                    [
                        // new WorkflowExecutionOption
                        // {
                        //     AnalysisPromptCategoryId = Guid.Parse("FD49A0B2-403F-491F-A4C4-1C489758FB79"),
                        //     ChildExecutionOptions =
                        //     [
                        new WorkflowExecutionOption
                        {
                            AnalysisPromptCategoryId = Guid.Parse("D187624D-8AF7-4495-BF7B-00084A63372E"),
                        },
                        new WorkflowExecutionOption
                        {
                            AnalysisPromptCategoryId = Guid.Parse("B12F3C18-2706-42BB-BF1A-B2AC3CB0BF3F"),
                            // ChildExecutionOptions =
                            // [
                            //     new WorkflowExecutionOption
                            //     {
                            //         AnalysisPromptCategoryId = Guid.Parse("6F1FDE2A-CAFC-4C4D-B909-655414C8C76E"),
                            //     },
                            // ]
                        },
                        new WorkflowExecutionOption
                        {
                            AnalysisPromptCategoryId = Guid.Parse("33CCCA43-E803-4FA2-AFC7-7C202DE5EA0C"),
                            // ChildExecutionOptions =
                            // [
                            //     new WorkflowExecutionOption
                            //     {
                            //         AnalysisPromptCategoryId = Guid.Parse("2EF85588-0B12-4FB8-9027-80D45CC38EC1"),
                            //     },
                            // ]
                        },
                    ]
                },
                // ]
                // },
                // new WorkflowExecutionOption
                // {
                //     AnalysisPromptCategoryId = Guid.Parse("159D0655-40AE-4DED-8C83-0FFFF69A7704"),
                // },
            ]
        };

    await appDbContext.WorkflowExecutionOptions.AddRangeAsync(executionOptions);
}

/// <summary>
/// Seeds analysis workflows
/// </summary>
private static async ValueTask SeedAnalysisWorkflowsAsync(AppDbContext appDbContext)
{
    var templateWorkflow = new AnalysisWorkflow
    {
        Id = Guid.Parse("fb5653f6-f8e7-47fa-ab70-5e693de92ea0"),
        Name = "TemplateWorkflow",
        Type = WorkflowType.Template,
        EntryExecutionOptionId = Guid.Parse("E4B16AEB-41C3-4E50-AB0B-8A883AE397C1"),
    };

    await appDbContext.AnalysisWorkflows.AddRangeAsync(templateWorkflow);
}

/// <summary>
/// Seeds feedback analysis workflows
/// </summary>
private static async ValueTask SeedFeedbackAnalysisWorkflowsAsync(AppDbContext appDbContext)
{
    var templateFeedbackAnalysisWorkflow = new List<FeedbackAnalysisWorkflow>
    {
        new()
        {
            Id = Guid.Parse("fb5653f6-f8e7-47fa-ab70-5e693de92ea0"),
            ProductId = Guid.Parse("751d1c24-24c2-45aa-9eba-383de543b34b")
        },
    };

    await appDbContext.FeedbackAnalysisWorkflows.AddRangeAsync(templateFeedbackAnalysisWorkflow);
}

}