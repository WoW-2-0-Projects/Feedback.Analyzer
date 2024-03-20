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
                Name = "iPhone",
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
                         ## Instructions

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
                Version = 1,
                Revision = 0,
            },
            new()
            {
                Id = Guid.Parse("3ca01475-d736-4ac3-a326-a2580110ee0c"),
                CategoryId = Guid.Parse("787BB696-5057-4840-9161-770AD88FFA9B"),
                Prompt = """
                         ## Instructions

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
                Version = 2,
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
                Version = 3,
                Revision = 0,
            },
            new()
            {
                Id = Guid.Parse("2ba01475-d636-4ac3-a326-a2580112ee0c"),
                CategoryId = Guid.Parse("28C2137D-E6F7-440D-9513-1EE2E0B36530"),
                Prompt = """
                         ## Instructions

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
                Version = 4,
                Revision = 0,
            },
            new()
            {
                Id = Guid.Parse("551d1c24-24c2-45aa-9eba-383de543b24b"),
                CategoryId = Guid.Parse("B12F3C18-2706-42BB-BF1A-B2AC3CB0BF3F"),
                Prompt = """
                         ## Instructions

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
                Id = Guid.Parse("c95c1641-8f99-40c8-b42d-948d8671fc3c"),
                CategoryId = Guid.Parse("2ef85588-0b12-4fb8-9027-80d45cc38ec1"),
                Prompt = """
                    Actionable Questions Analysis from User Feedback:
                    Objective: Examine user feedback to identify actionable questions that indicate areas for improvement, clarification, or further inquiry. These questions should be ones that can lead to tangible changes or responses from the product or service team.

                    Instructions:
                    Identify Actionable Questions:
                    Review Feedback: Carefully analyze the user feedback provided.

                    Extract Actionable Questions: Identify questions that imply a need for action, such as feature requests, bug reports, or clarifications about product usage.

                    Analyze and Prioritize:
                    Assess Urgency and Relevance: Evaluate the importance and immediacy of each actionable question based on the product's current objectives and user needs.

                    Categorize by Theme: Organize the questions into thematic categories such as Usability, Functionality, Customer Support, etc., to streamline the response process.

                    Document Recommendations:
                    Propose specific actions or responses to each identified question, considering the potential impact on the user experience and product development.
                    Context Provided:
                    Product Description: {{$productDescription}}
                    User Feedback: {{$userFeedback}}
                    Expected Output:
                    Actionable Questions: A list of identified questions from the user feedback that are directly actionable, accompanied by a brief analysis of their urgency, relevance, and proposed actions.
                    Result:
                    [Actionable Questions Analysis]: Present a structured analysis of each actionable question identified in the user feedback. This should include the question itself, its categorized theme, the assessed urgency, and recommended actions or responses.
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
                Id = Guid.Parse("7ac803e7-8fcc-4e4f-9a5e-c1e79bb32d4f"),
                CategoryId = Guid.Parse("6f1fde2a-cafc-4c4d-b909-655414c8c76e"),
                Prompt = """
                    ##Detailed Feedback Analysis for Actionable Insights:
                    ##Objective: Analyze user comments to extract and summarize their overall meaning, and categorize the feedback as either generic, specific, and/or actionable.
                         
                    ##Instructions:
                       ##Summarize and Interpret Feedback:
                         
                         Review User Comment: Carefully read the feedback provided.
                         Draft Overall Summary: Write a concise summary that captures the essential message or sentiment from the feedback.
                       ##Evaluate and Classify Feedback:
                         
                         Assess Nature of Feedback: Determine if the feedback is generic (broad concerns or opinions) or specific (concrete, detailed observations).
                         Determine Actionability: Identify if the feedback contains clear, implementable suggestions or improvements.
                       ##Document Analysis and Recommendations:
                         
                         Based on the nature and content of the feedback, outline any direct actions that can be taken or consider broader implications for systemic improvements.
                   
                  ##Product Description:
                     {{$productDescription}}
                 
                 ##User Feedback:
                     {{$userFeedback}}
                 
                  ##Expected Output:
                       ##Feedback Summary: A concise interpretation capturing the core message of the user's feedback.
                       ##Classification:
                         Nature: [Generic/Specific] – Indicate whether the feedback addresses broad concepts or specific details.
                         Actionability: [Actionable/Non-Actionable] – State whether the feedback presents clear steps for improvement.
                    ##Result:
                         In this section, you would present the analysis of the user's comment based on the instructions provided. For example:
                         
                       ##Feedback Summary: ""The user appreciates the mouse but suggests improvements to the wheel button for better functionality.""
                       ##Classification:
                         Nature: Specific – The feedback points to a particular aspect of the product.
                         Actionability: Actionable – The feedback includes a specific suggestion for product enhancement.
                """,
                Version = 1,
                Revision = 0
            },
            new()
            {
                Id = Guid.Parse("ad52fe38-23f4-4aa2-bf60-c8a610089a89"),
                CategoryId = Guid.Parse("d187624d-8af7-4495-bf7b-00084a63372e"),
                Prompt = """
                    ## Advanced Opinion Mining
                
                    Analyze the sentiment of the following user feedback based on the detailed product description provided. Classify the overall sentiment into 'Positive', 'Negative', or 'Neutral'. Consider nuanced language, contextual clues, and specific product attributes mentioned in the feedback.
                    
                    Product Description: {{$productDescription}}
                    
                    User Feedback: {{$userFeedback}}
                    
                    Ensure the response is concise and directly correlates to the sentiments expressed in the feedback.
                        
                    Result:
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
                        new WorkflowExecutionOption
                        {
                            AnalysisPromptCategoryId = Guid.Parse("FD49A0B2-403F-491F-A4C4-1C489758FB79"),
                            ChildExecutionOptions =
                            [
                                new WorkflowExecutionOption
                                {
                                    AnalysisPromptCategoryId = Guid.Parse("D187624D-8AF7-4495-BF7B-00084A63372E"),
                                },
                                new WorkflowExecutionOption
                                {
                                    AnalysisPromptCategoryId = Guid.Parse("B12F3C18-2706-42BB-BF1A-B2AC3CB0BF3F"),
                                    ChildExecutionOptions =
                                    [
                                        new WorkflowExecutionOption
                                        {
                                            AnalysisPromptCategoryId = Guid.Parse("6F1FDE2A-CAFC-4C4D-B909-655414C8C76E"),
                                        },
                                    ]
                                },
                                new WorkflowExecutionOption
                                {
                                    AnalysisPromptCategoryId = Guid.Parse("33CCCA43-E803-4FA2-AFC7-7C202DE5EA0C"),
                                    ChildExecutionOptions =
                                    [
                                        new WorkflowExecutionOption
                                        {
                                            AnalysisPromptCategoryId = Guid.Parse("2EF85588-0B12-4FB8-9027-80D45CC38EC1"),
                                        },
                                    ]
                                },
                            ]
                        },
                    ]
                },
                new WorkflowExecutionOption
                {
                    AnalysisPromptCategoryId = Guid.Parse("159D0655-40AE-4DED-8C83-0FFFF69A7704"),
                },
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