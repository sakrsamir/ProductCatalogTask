using System.Data.Entity;

namespace ProductCatalog.Models
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}