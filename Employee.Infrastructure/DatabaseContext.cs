using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Employee.Domain.Entities;

namespace Employee.Infrastructure
{
    public class DatabaseContext: DbContext
    {
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DatabaseContext():base("DefaultConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
