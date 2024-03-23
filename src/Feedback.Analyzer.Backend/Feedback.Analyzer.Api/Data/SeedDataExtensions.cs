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
                FirstName = "Bob",
                LastName = "Richard",
                EmailAddress = "tastBobRichard@gmail.com",
                PasswordHash = "$2a$12$LxSqe5AE7AtglesHHK5NROFdJQdA1r1XKqhzg4q/tMTZjVEH0PNSK", //asdf1234
                Role = Role.Admin
            },
            new()
            {
                Id = Guid.Parse("6357D344-CB69-4FAA-81C5-AC0FC59AE0F9"),
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
                Id = Guid.Parse("C97801A8-2CBA-4BD8-958D-8B5F014B74DE"),
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
                ClientId = Guid.Parse("6357D344-CB69-4FAA-81C5-AC0FC59AE0F9"),
            },
        };

        await appDbContext.Organizations.AddRangeAsync(organizations);
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
               Id = Guid.Parse("438e4b3c-c641-4a03-ae6b-5091b1b436ea"),
               OrganizationId = Guid.Parse("C97801A8-2CBA-4BD8-958D-8B5F014B74DE"),
               Name = "Razor Viper Ultimate",
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
            
            // Real customer feedbacks
            new()
            {
                Id = Guid.Parse("12722129-1d17-4de5-992a-a450247c3211"),
                ProductId = Guid.Parse("438e4b3c-c641-4a03-ae6b-5091b1b436ea"),
                Comment = """
                          I laid my hands on the Viper in a local store and on the spot it felt rather flat. I think I prefer something with more hump and side area to grip onto. right now I have a basilisk v2 and think it's definetely more comfy, but that grip could still be better.
                          
                          The Synapse software is extremely greedy though. 300MB HD space and about 200MB RAM is ludicrous for a gloryfied mouse driver.
                          """,
                UserName = "Silverspark"
            },
            new()
            {
                Id = Guid.Parse("03e3827d-36d2-474f-ab76-7655833b090f"),
                ProductId = Guid.Parse("438e4b3c-c641-4a03-ae6b-5091b1b436ea"),
                Comment = """
                          Yup pretty flat, very fk1 like at the hump. Some people prefer the extra freedom and it was definitely interesting to try out.
                          
                          I already had the software installed for the mini so yeah pretty heavy but seeing how sophisticated it is and common to all razer peripherals it's acceptable i think.
                          """,
                UserName = "azami88n"
            },
            new()
            {
                Id = Guid.Parse("E7F7AFE2-7172-430C-A503-D55D84C11131"),
                ProductId = Guid.Parse("438e4b3c-c641-4a03-ae6b-5091b1b436ea"),
                Comment = """
                          Battery life is great? Are you sure? Been using one here for two weeks and well..after a day or two of only using it a few hours, it loses quite abit.
                          
                          I'm down to 55% after using the mouse for only a few hours. I have RGB disabled and Low Power Mode set to 100.
                          
                          This isn't my first either, I got another one and it's similar.
                          
                          The mouse does go to sleep, as it's wakeup is very noticble.
                          
                          I kinda think it's a sub optimal shape, but I'm getting used to it as well..and not impressed.
                          
                          my G Pro could go an entire month with medium gaming and only be at around 15-20% by then.
                          
                          My Razer, which I charge frequently due to the dock, is just...pathetic.
                          """,
                UserName = "Mkilbride"
            },         
            new()
            {
                Id = Guid.Parse("88DE3171-AB8A-49E7-A854-A5A77A4D94F7"),
                ProductId = Guid.Parse("438e4b3c-c641-4a03-ae6b-5091b1b436ea"),
                Comment = """
                          The quality of the Viper Ultimate is noticeably higher than the wired version for anyone who loves the shape and not the price. It may have just been my copy but the wired Viper wasn't balanced and had a slight wobble when flat on the desk. My wired Viper also would randomly cut out for a split second three or four times a week with only a few months of use.
                          
                          The Viper Ultimate is the best wireless mouse I've tested though, feels great, looks great, performs as expected.
                          """,
                UserName = "No_Specific2566"
            },
            new()
            {
                Id = Guid.Parse("33950F44-8439-456A-ABCE-40D4C10A24CD"),
                ProductId = Guid.Parse("438e4b3c-c641-4a03-ae6b-5091b1b436ea"),
                Comment = """
                          Huge thanks to Razer and to u/Razer-Right for providing me this sample free of charge. This review will be my own toughts. This mouse has received plenty of full reviews so i'll keep it brief and focus on a few things that didn't get much attention.
                          
                          Background for reference: hand size: 20x10cm, preferred grip style: relaxed claw. Main mice: gpw, zowie s2 and ec2. Game genre: FPS (CSGO, black squad, valorant). Tried or designed over 60 mice shapes in search of an ideal one.
                          
                          === Shape:
                          
                          ++ The side slant (about 7 degrees at the grip) feels quite more natural than the gpw's slightly negative slant of 1-2degrees (inverted slant).
                          
                          +The hump is a lot flatter feeling (fk1 style) than the hump on logitech GPW, so the palm "rests" on that hump rather than grips it. As a result, it might appeal more to a fingertip-claw grip style especially with the slanted sides While gpw tends to favor relaxed claw and palm grip.
                          
                          +Button groves are good, though they might be better slightly less off-centered
                          
                          -- The side curvature is hourglass shaped. It felt a bit odd for my grip style (i rest my ring and pinky on the middle- front section) in my experience, fully flush, straight, and slightly slanted sides like on the steelseries aerox/rival 110 provide the best comfort-control ratio. Agressive claw/fingertip is fine while resting the pinky finger on the back of the sides, without using the flaring front. (Don't get me wrong: it feels okay to use in relaxed claw and i did perform very well with it, it just feels like a sub optimal shape choice that needs getting used to.) Maybe they made this trade to accomodate more hand sizes but that also means it would not feel perfect for most.
                          
                          === Build quality:
                          
                          ++ Overall build quality is solid. No creaking, no plastic squeak when pressing main buttons like it was sometimes the case with the viper mini.
                          
                          ++ Optical switches feel slightly different but they're fine. I don't think it's so bad as to keep someone from liking this mouse.
                          
                          ++ Weight is just right for a wireless mouse this size
                          
                          ++ Coating is a bit rough textured, but most of the grip happens on the rubber sides anyway.
                          
                          ++ Sensor is flawless.
                          
                          ++ Scroll wheel is perfect.
                          
                          ++ Feet are great.
                          
                          +Side buttons are recessed and hard to hit by accident. But also harder to reach on the go as with something like zowie s2's huge side buttons. I personally never use them so it's a welcome change.
                          
                          +Rubber sides do feel different but they didnt make much of a difference in use. It reliefs some pressure on the fingers and will prevent plastic hurting over long use sessions. I have concerns about how well it will hold with time (some rubbers fall apart with time, let alone mechanical stress, i'll just believe razer used a high quality rubber there)
                          
                          -- The rubber is slightly raised, there is a slight gap between the mousepad and the rubber that can be gripped and it can be noticeable if you grip the mouse low with your thumb. (Its the same problem with g403 but much less noticeable).
                          
                          -- Rubber prevents side grips from sticking unless using rubber specific adhesives
                          
                          -- The squared ptfe foot around the sensor has slightly sticky edges that attracts dust hair and it eventually gets inside the sensor hole. (It only happend two to three times over a month use period; maybe because the stickers are new?)
                          
                          === Battery/charging:
                          
                          ++ Battery life is great. It holds for two weeks under moderate to heavy use. Edit: since this surprised some people, i used rgb off but also turned off the mouse instezd of letting it idle, this can save 100 hours of idle power use weekly (if you count 10hours daily use). But further battery cycles might be necessary to objectively have a saying on this matter)
                          
                          ++ The charging dock's implementation is flawless. It's very practical and looks sleek.
                          
                          -- The charging port on the front of the mouse can't accomodate a magnetic usb charging cable as it is positioned low and deep.
                          
                          -- Power on switch is awkwardly positioned and sized. I'm used to gpw and the switch is large, and switching direction is natural so powering on and off quickly with one hand is possible. Viper powering on/off needs two hands.
                          """,
                UserName = "azami88m"
            }
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
                         ## Concepts

                         Customer feedback : The customer's feedback comment about the product
                         Personal Inforation Redaction : Identifying and removing personally identifiable information from the feedback

                         ## Instructions

                         Analyze customer feedback, understand the language, identify customer personal information and redact it from the feedback.

                         Requirements :
                         1. redact the content with asterisk symbol
                         2. output must be in a single string format, without any prefixes or postfixes, just the redacted feedback

                         ## Examples

                         1. Without any personal information:

                         Customer Feedback : I recently switched to the Logitech MX Master 3 for my home office, and it has been a fantastic
                         experience. The ergonomic design fits perfectly in my hand, reducing fatigue during long work sessions. The scroll wheel is
                         smooth and precise, making document navigation a breeze. I highly recommend this mouse to anyone looking for a high-quality,
                         comfortable, and reliable option.

                         Result :  I recently switched to the Logitech MX Master 3 for my home office, and it has been a fantastic experience. The
                         ergonomic design fits perfectly in my hand, reducing fatigue during long work sessions. The scroll wheel is smooth and
                         precise, making document navigation a breeze. I highly recommend this mouse to anyone looking for a high-quality,
                         comfortable, and reliable option.

                         Reasoning : This customer feedback doesn't contain any personal information, so no redaction is needed.

                         2. With 2 PII Words:

                         Customer Feedback : I've been using the Logitech MX Master 3 for my graphic design projects and it's a game changer. The
                         customizable buttons have streamlined my workflow significantly. However, I've had some issues syncing my settings across
                         devices. Could you update my email address from jane.doe@example.com to my new one? Also, please change the registered phone
                         number from 555-6789 to my current number.

                         Result : I've been using the Logitech MX Master 3 for my graphic design projects and it's a game changer. The
                         customizable buttons have streamlined my workflow significantly. However, I've had some issues syncing my settings across
                         devices. Could you update my email address from ******************** to my new one? Also, please change the registered
                         phone number from ******** to my current number.

                         Reasoning : Here, we redacted the email address and phone number from the feedback.

                         3. With Some Advanced Case:

                         Customer Feedback : I'd like to provide feedback on my Logitech MX Master 3 mouse. It's been an integral tool for my
                         video editing tasks. However, I had a challenging time with the Logitech Options software; it wouldn't recognize my
                         device initially. For verification, the product was registered under Michael Clark, with the transaction ID
                         9876-5432-1001. Additionally, I had filled out the warranty form with my previous address, 7890 Elm Street. I need
                         assistance in updating this to avoid service delays.

                         Result : I'd like to provide feedback on my Logitech MX Master 3 mouse. It's been an integral tool for my
                         video editing tasks. However, I had a challenging time with the Logitech Options software; it wouldn't recognize my
                         device initially. For verification, the product was registered under ******* *****, with the transaction ID
                         ****-****-****. Additionally, I had filled out the warranty form with my previous address, **** *** Street. I need
                         assistance in updating this to avoid service delays.

                         Reasoning : Here, we redacted the name, transaction ID, and address from the feedback.

                         Reasoning :

                         ## Input

                         Customer feedback - {{$customerFeedback}}
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
        var razorWorkflow = new AnalysisWorkflow()
        {
            Id = Guid.Parse("cc9bbbbf-0a1a-40d9-b163-1e032aa56c7d"),
            Name = "RazorWorkflow",
            Type = WorkflowType.Template,
            EntryExecutionOptionId = Guid.Parse("E4B16AEB-41C3-4E50-AB0B-8A883AE397C1")
        };

        await appDbContext.AnalysisWorkflows.AddAsync(razorWorkflow);
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
                Id = Guid.Parse("cc9bbbbf-0a1a-40d9-b163-1e032aa56c7d"),
                ProductId = Guid.Parse("438e4b3c-c641-4a03-ae6b-5091b1b436ea")
            }
        };

        await appDbContext.FeedbackAnalysisWorkflows.AddRangeAsync(templateFeedbackAnalysisWorkflow);
    }
}