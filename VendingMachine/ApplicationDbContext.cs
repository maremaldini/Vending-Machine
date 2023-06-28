using System.Data.Entity;

namespace iQuest.VendingMachine
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext()
        {

        }
    }
}