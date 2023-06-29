using Bogus;
using DriversManagement.Models.Data.Entities;

namespace DriversManagement.Utilities
{
    public static class FakerDataGenerator
    {
        public static List<User> CreateUsers(int usersCount)
        {
            var users = new List<User>();

            for (int i = 0; i < usersCount; i++)
            {
                var faker = new Faker("fa");
                var user = new User()
                {
                    Name = faker.Name.FullName(),
                    Mobile = faker.Phone.PhoneNumberFormat().Replace(" ",""),
                    RoleId = 2,
                    Password = faker.Random.Number(10000,100000).ToString()
                };

                users.Add(user);
            }

            return users;
        }
    }
}
