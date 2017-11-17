namespace Identity.Migrations
{
    using Identity.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Identity.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var roleNames = new[] { Role.Admin, Role.Editor };
            foreach (var roleName in roleNames)
            {
                if (context.Roles.Any(r => r.Name == roleName)) continue;

                // Create role
                var role = new IdentityRole { Name = roleName };
                var result = roleManager.Create(role);
                if (!result.Succeeded) {
                    throw new Exception(string.Join("\n", result.Errors));
                }
            }

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var emails = new[] { "user@lexicon.se", "editor@lexicon.se", "admin@lexicon.se", "dimitris@lexicon.se" };
            foreach (var email in emails)
            {
                if (context.Users.Any(u => u.UserName == email)) continue;

                // Create user
                var user = new ApplicationUser { UserName = email, Email = email };
                var result = userManager.Create(user, "foobar");
                if (!result.Succeeded) {
                    throw new Exception(string.Join("\n", result.Errors));
                }
            }

            var adminUser = userManager.FindByName("admin@lexicon.se");
            userManager.AddToRole(adminUser.Id, Role.Admin);

            var editorUser = userManager.FindByName("editor@lexicon.se");
            userManager.AddToRole(editorUser.Id, Role.Editor);

            var dimitris = userManager.FindByName("dimitris@lexicon.se");
            userManager.AddToRoles(dimitris.Id, Role.Admin, Role.Editor);                    
        }
    }
}
