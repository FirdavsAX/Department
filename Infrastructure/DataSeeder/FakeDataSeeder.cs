using Application.DTOs.CategoryDto;
using Bogus;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.DataSeeder;

public static class FakeDataSeeder
{
    public static async Task FakeCategories(ApplicationDbContext context, int number)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context), "ApplicationDbContext cannot be null.");
        }
        if (context.Categories.Any())
        {
            return; // If categories already exist, skip seeding
        }
        List<string> categoryNames = new List<string>()
            {
                "Ilm-fan (Science)", "Texnologiya (Technology)","Ta’lim (Education)","Jamiyat (Society)","Sog‘liqni saqlash (Health)",
                "Iqtisodiyot (Economics)","San’at va madaniyat (Art & Culture)","Tarix (History)","Psixologiya (Psychology)","Adabiyot (Literature)",
                "Siyosat (Politics)","Qonunchilik va huquq (Law & Legislation)","Motivatsiya va rivojlanish (Motivation & Self-development)",
                "Tadbirkorlik (Entrepreneurship)","Atrof-muhit va ekologiya (Environment & Ecology)","Sport va sog‘lom turmush (Sports & Healthy Lifestyle)",
                "Chet tillar va tarjima (Languages & Translation)","Diniy bilimlar (Religion)","Startaplar va innovatsiyalar (Startups & Innovations)",
                "IT va dasturlash (IT & Programming)","Media va jurnalistika (Media & Journalism)","Kino va televideniye (Cinema & Television)"
            };
        // Seed Categories
        Faker<Category> faker = new Faker<Category>()
            .RuleFor(c => c.Name, f => f.PickRandom(categoryNames))
            .RuleFor(c => c.Description, f => f.Lorem.Lines(3));

        var result = faker.Generate(number);
        await context.Categories.AddRangeAsync(result);
        await context.SaveChangesAsync();
    }
    public static async Task FakeMembers(ApplicationDbContext context, int number)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context), "ApplicationDbContext cannot be null.");
        }
        if (context.Members.Any())
        {
            return; // If members already exist, skip seeding
        }
        Faker<Member> faker = new Faker<Member>()
            .RuleFor(m => m.FullName, f => f.Name.FullName())
            .RuleFor(m => m.Degree, f => f.Name.JobTitle())
            .RuleFor(m => m.Info, f => f.Lorem.Paragraph(2))
            .RuleFor(m => m.PhotoUrl, f => f.Image.PicsumUrl());

        var result = faker.Generate(number);
        await context.Members.AddRangeAsync(result);
        await context.SaveChangesAsync();
    }

}
