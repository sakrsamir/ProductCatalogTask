using System.Data.Entity;

namespace ProductCatalog.Models
{
    public class ApplicationDbContext:DbContext
    {
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}