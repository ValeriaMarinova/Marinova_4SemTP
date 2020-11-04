using AbstractFactoryDatabaseImplement.Models;
using AbstractFactoryListImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace AbstractFactoryDatabaseImplement
{
    public class AbstractFactoryDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=AbstractFactoryDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<AutoPart> AutoParts { set; get; }
        public virtual DbSet<Product> Products { set; get; }
        public virtual DbSet<ProductAutoPart> ProductAutoParts { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
    }
}
