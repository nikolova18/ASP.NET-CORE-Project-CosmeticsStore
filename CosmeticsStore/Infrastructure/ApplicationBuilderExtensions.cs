namespace CosmeticsStore.Infrastructure
{
    using System.Linq;
    using CosmeticsStore.Data;
    using CosmeticsStore.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;


    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<ApplicationDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(ApplicationDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category { Name = "Skin Care" },
                new Category { Name = "Hair Care" },                 
                new Category { Name = "Face care" },
                new Category { Name = "Nail care" },
                new Category { Name = "MakeUp" },
                new Category { Name = "Fragrance" },
                new Category { Name = "Hygiene" }
            });

            data.SaveChanges();
        }
    }
}
