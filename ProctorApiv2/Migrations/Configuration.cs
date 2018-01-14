namespace ProctorApiv2.Migrations
{
    using ProctorApiv2.Models;
    using ProctorApiv2.Utils;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<ProctorV2Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ProctorV2Context context)
        {
            SeedUsers(context);
        }

        private void SeedUsers(ProctorV2Context db)
        {
            var userManager = new ApplicationUserManager(new UserStore<User>(db));

            var admins = new[]
            {
                new User { UserName = "admin", Email = "testuser@unknown.com", EmailConfirmed = true, IsActive = true },
                new User { UserName = "admin2", Email = "testuser2@unknown.com", EmailConfirmed = true, IsActive = true }
            };

            foreach (var user in admins)
            {
                if (userManager.Users.Any(i => i.UserName == user.UserName))
                    continue;
                userManager.Create(user, "Admin1234!");
            }

        }
    }
}
