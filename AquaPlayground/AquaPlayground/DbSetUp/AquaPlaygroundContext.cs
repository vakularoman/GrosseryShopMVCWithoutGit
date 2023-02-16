namespace AquaPlayground
{
    using AquaPlayground.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class AquaPlaygroundContext : IdentityDbContext<User, Role, long>
    {
        public AquaPlaygroundContext(DbContextOptions<AquaPlaygroundContext> options)
         : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Nutrition> Nutritions { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<UserToPaymentSystem> UsersToPaymentSystems { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<ProductToTag> ProductsToTags { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<PaymentSystem> PaymentSystems { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<CartProduct> CartProducts { get; set; }
    }
}
