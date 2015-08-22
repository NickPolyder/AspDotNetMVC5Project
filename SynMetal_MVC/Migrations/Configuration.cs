namespace SynMetal_MVC.Migrations
{

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
       
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            #region Roles
            if (!context.Roles.Any(u => u.Name == "Administrator"))
            {
                roleManager.Create(new IdentityRole { Name = "Administrator" });

            }
            if (!context.Roles.Any(u => u.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }
            if (!context.Roles.Any(u => u.Name == "SimpleUser"))
            {
                roleManager.Create(new IdentityRole { Name = "SimpleUser" });
            }
            #endregion

            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            #region Users
            if (!context.Users.Any(u => u.UserName =="Admin"))
            {
             
                var user = new ApplicationUser { UserName = "Admin" , Email = "Admin@synmetal.gr", EmailConfirmed=true };
                manager.Create(user, "Adm1n1str@t0r");
                manager.AddToRole(user.Id, "Administrator");
            }
            if (!context.Users.Any(u => u.UserName == "SomeUser@Moderator.com"))
            {
              
                var user = new ApplicationUser { UserName = "User", Email = "SomeUser@Moderator.com", EmailConfirmed = true };
                manager.Create(user, "M0der@t0r");
                manager.AddToRole(user.Id, "Moderator");
            }
            #endregion
          

            #region defaults
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            #endregion
        }
    }
}
