using System.Data.Entity.Migrations;

namespace Pinger.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Model.SiteContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Model.SiteContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
