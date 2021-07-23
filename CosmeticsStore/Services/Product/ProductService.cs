namespace CosmeticsStore.Services.Product
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using CosmeticsStore.Data;
    using CosmeticsStore.Models;


    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext data;

        public ProductService(ApplicationDbContext data)
            => this.data = data;

        public ProductQueryServiceModel All(
            string brand,
            string searchTerm,
            ProductSorting sorting,
            int currentPage,
            int productsPerPage)
        {
            var productsQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                productsQuery = productsQuery.Where(p => p.Brand == brand);
            }


            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                      (p.Brand + " " + p.Name).ToLower().Contains(searchTerm.ToLower()) ||
                      p.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            productsQuery = sorting switch
            {
                ProductSorting.Price => productsQuery.OrderBy(p => p.Price),
                ProductSorting.Quantity => productsQuery.OrderBy(p => p.Quantity),
                ProductSorting.BrandAndName => productsQuery.OrderBy(p => p.Brand).ThenBy(p => p.Name),
                ProductSorting.DateCreated or _ => productsQuery.OrderByDescending(p => p.Id)
            };

            var totalProducts = productsQuery.Count();

            var products = productsQuery
                .Skip((currentPage - 1) * productsPerPage)
                .Take(productsPerPage)
                .Select(p => new ProductServiceModel
                {
                    Id = p.Id,
                    Brand = p.Brand,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    Category = p.Category.Name
                })
                .ToList();

            return new ProductQueryServiceModel
            {
                TotalProducts = totalProducts,
                CurrentPage = currentPage,
                ProductsPerPage = productsPerPage,
                Products = products
            };
        }

        public IEnumerable<string> AllProductBrands()
         => this.data
                .Products
                .Select(c => c.Brand)
                .Distinct()
                .OrderBy(br => br)
                .ToList();
    }
}
