
using ProductManagementModule;
namespace ProductManagementModule
{
    using Microsoft.EntityFrameworkCore;
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<Product> products { get; set; }
    }
}
