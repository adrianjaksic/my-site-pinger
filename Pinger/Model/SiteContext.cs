using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Pinger.Model
{
    public class SiteContext : DbContext
    {
        public SiteContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<Site> Sites { get; set; }
        public DbSet<SiteStatus> SiteStatuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

}
