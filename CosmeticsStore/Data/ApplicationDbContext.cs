namespace CosmeticsStore.Data
{
    using CosmeticsStore.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; init; }

        public DbSet<Category> Categories { get; init; }

        public DbSet<Dealer> Dealers { get; init; }

        public DbSet<Cart> Carts { get; init; }

        public DbSet<CartItem> CartItems { get; init; }

        public DbSet<Delivery> Deliveries { get; init; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //products creating
            builder
                .Entity<Product>()
                .HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Product>()
                .HasOne(c => c.Dealer)
                .WithMany(d => d.Products)
                .HasForeignKey(p => p.DealerId)
                .OnDelete(DeleteBehavior.Restrict);


            //dealer creating
            builder
                .Entity<Dealer>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Dealer>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            //cartitems creating
            builder
                .Entity<CartItem>()
                .HasOne<Product>()
                .WithOne()
                .HasForeignKey<CartItem>(d => d.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            //cart creating
            builder
                .Entity<Cart>()
                .HasOne(u=>u.User)
                .WithOne(c => c.Cart)
                .HasForeignKey<Cart>(c=>c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Cart>()
                .HasMany(ci => ci.CartItems)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);


            //delivery creating
            builder
                .Entity<Delivery>()
                .HasOne<Cart>()
                .WithOne()
                .HasForeignKey<Delivery>(d => d.CartId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }
    }
}
