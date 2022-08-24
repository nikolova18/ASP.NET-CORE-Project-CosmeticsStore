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

        public DbSet<Delivery> Deliveries { get; init; }

        public DbSet<User> Users { get; init; }



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


             builder
                .Entity<Product>()
                .HasOne(c => c.User)
                .WithMany(d => d.Products)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            //dealer creating
            builder
                .Entity<Dealer>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Dealer>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            //delivery creating
            builder
                .Entity<Delivery>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Delivery>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }
    }
}
