namespace SynMetal_MVC.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SynMetal_MVC.Models.SynMetalEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SynMetal_MVC.Models.SynMetalEntities";
        }

        protected override void Seed(SynMetal_MVC.Models.SynMetalEntities context)
        {
            SynMetal_MVC.Models.DataHelpers.CreateCategories();
            SynMetal_MVC.Models.DataHelpers.CreateNews();
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
        }
    }
}
