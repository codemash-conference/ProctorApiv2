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
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ProctorV2Context context)
        {
            SeedRoles(context);
            SeedUsers(context);
            SeedRooms(context);
            SeedTags(context);
            SeedSessionTypes(context);
        }

        private void SeedRoles(ProctorV2Context db)
        {
            var roleManager = new RoleManager<Role>(new RoleStore<Role>(db));

            var roles = new[]
            {
                new Role { Name = "Admin" },
                new Role { Name = "Volunteers" },
                new Role { Name = "Board Members" },
                new Role { Name = "Volunteer Admin" },
                new Role { Name = "Everyone" },
                new Role { Name = "Committee Chairs" }
            };

            foreach (var role in roles)
            {
                if (roleManager.Roles.Any(i => i.Name == role.Name))
                    continue;
                roleManager.Create(role);
            }

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
                userManager.AddToRole(user.Id, "Admin");
                userManager.AddToRole(user.Id, "Everyone");
            }

        }
        private void SeedRooms(ProctorV2Context db)
        {
            var rooms = new[]
            {
                new Room { Name = "Acacia" },
                new Room { Name = "Aloeswood" },
                new Room { Name = "Banyan" },
                new Room { Name = "Coat Check" },
                new Room { Name = "Crown Palm" },
                new Room { Name = "Cypress" },
                new Room { Name = "Great Wolf Lodge" },
                new Room { Name = "Guava" },
                new Room { Name = "Indigo Bay" },
                new Room { Name = "Ironwood" },
                new Room { Name = "KidzMash" },
                new Room { Name = "KidzMash Registration" },
                new Room { Name = "Leopardwood" },
                new Room { Name = "Mangrove" },
                new Room { Name = "Nile" },
                new Room { Name = "Orange" },
                new Room { Name = "Portia" },
                new Room { Name = "Registration" },
                new Room { Name = "Rosewood" },
                new Room { Name = "Sagewood" },
                new Room { Name = "Salon A" },
                new Room { Name = "Salon B" },
                new Room { Name = "Salon C" },
                new Room { Name = "Salon D" },
                new Room { Name = "Salon E" },
                new Room { Name = "Salon F" },
                new Room { Name = "Salon G" },
                new Room { Name = "Salon H" },
                new Room { Name = "Sponsor Booth" },
                new Room { Name = "Suite 1" },
                new Room { Name = "Suite 6" },
                new Room { Name = "Swag Booth" },
                new Room { Name = "Tamarind" },
                new Room { Name = "The Reserve" },
                new Room { Name = "Various Rooms" },
                new Room { Name = "Wisteria" },
                new Room { Name = "Zambezi" },
                new Room { Name = "Zebrawood" }
            };

            foreach (var room in rooms)
            {
                if (db.Rooms.Any(i => i.Name == room.Name))
                    continue;
                db.Rooms.Add(room);
                db.SaveChanges();
            }

        }

        private void SeedTags(ProctorV2Context db)
        {
            var tags = new[]
            {
                new Tag { Name = ".NET" },
                new Tag { Name = "Cloud/Big Data" },
                new Tag { Name = "Design (UI/UX/CSS)" },
                new Tag { Name = "Events" },
                new Tag { Name = "Functional Programming" },
                new Tag { Name = "Hardware" },
                new Tag { Name = "Java" },
                new Tag { Name = "JavaScript" },
                new Tag { Name = "Mobile" },
                new Tag { Name = "Other" },
                new Tag { Name = "Ruby/Rails" },
                new Tag { Name = "Security" },
                new Tag { Name = "Soft Skills / Business" },
                new Tag { Name = "Testing" }
            };

            foreach (var tag in tags)
            {
                if (db.Tags.Any(i => i.Name == tag.Name))
                    continue;
                db.Tags.Add(tag);
                db.SaveChanges();
            }

        }

        private void SeedSessionTypes(ProctorV2Context db)
        {
            var sessionTypes = new[]
            {
                new SessionType { Name = "CodeMash Schedule Item" },
                new SessionType { Name = "General Session" },
                new SessionType { Name = "Kidz Mash" },
                new SessionType { Name = "Pre-Compiler" },
                new SessionType { Name = "Sponsor Session" },
                new SessionType { Name = "Static Session" }
            };

            foreach (var sessionType in sessionTypes)
            {
                if (db.SessionType.Any(i => i.Name == sessionType.Name))
                    continue;
                db.SessionType.Add(sessionType);
                db.SaveChanges();
            }

        }
    }
}
