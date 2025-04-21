namespace ProductManagementModule.Context
{
    using Microsoft.EntityFrameworkCore;
    using ProductManagementModule.Domain;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> products { get; set; }
        public DbSet<Cart> cart { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Role> role { get; set; }
        public DbSet<User> user { get; set; }
        public DbSet<OrderDetail> order_detail { get; set; }
    }
}
