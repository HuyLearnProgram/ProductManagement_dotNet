namespace ProductManagementModule.Context
{
    using Microsoft.EntityFrameworkCore;
    using ProductManagementModule.Domain;

    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<Product> products { get; set; }
    }
}
