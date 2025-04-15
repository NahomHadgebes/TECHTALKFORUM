using Bogus;
using TECHTALKFORUM.Models;

namespace TECHTALKFORUM.Data.Seed
{
    public static class SeedData
    {
        public static List<User> GenerateUsers(int count)
        {
            var faker = new Faker<User>()
                .RuleFor(u => u.Username, f => f.Internet.UserName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.PasswordHash, f => f.Internet.Password());

            return faker.Generate(count);
        }
        public static List<Channel> GenerateChannels(int count)
        {
            var faker = new Bogus.Faker<Channel>("en")
                .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
                .RuleFor(c => c.Description, f => f.Company.CatchPhrase());

            return faker.Generate(count);
        }
        public static List<Message> GenerateMessages(int count, List<User> users, List<Channel> channels)
        {
            var faker = new Faker<Message>("en")
                .RuleFor(m => m.Content, f => f.Lorem.Sentence())
                .RuleFor(m => m.CreatedAt, f => f.Date.Recent(10))
                .RuleFor(m => m.UserId, f => f.PickRandom(users).Id)
                .RuleFor(m => m.ChannelId, f => f.PickRandom(channels).Id);

            return faker.Generate(count);
        }



    }
}
