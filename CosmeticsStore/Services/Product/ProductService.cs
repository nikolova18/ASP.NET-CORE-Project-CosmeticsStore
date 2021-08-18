namespace CosmeticsStore.Services.Product
{
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using CosmeticsStore.Data;
    using CosmeticsStore.Data.Models;
    using CosmeticsStore.Models;
    using System.Collections.Generic;
    using CosmeticsStore.Services.Product.Models;

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext data;
        private readonly IConfigurationProvider mapper;

        public ProductService(ApplicationDbContext data,IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public ProductQueryServiceModel All(
            string brand = null,
            string searchTerm = null,
            ProductSorting sorting = ProductSorting.DateCreated,
            int currentPage=1,
            int productsPerPage = int.MaxValue,
            bool publicOnly = true)
        {
            var productsQuery = this.data.Products
                .Where(p=>!publicOnly || p.IsPublic);

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

            var products = GetProducts(productsQuery
                .Skip((currentPage - 1) * productsPerPage)
                .Take(productsPerPage));
                
            return new ProductQueryServiceModel
            {
                TotalProducts = totalProducts,
                CurrentPage = currentPage,
                ProductsPerPage = productsPerPage,
                Products = products
            };
        }
        public IEnumerable<LatestProductServiceModel> Latest()
        => this.data
                .Products
                .Where(p => p.IsPublic)
                .OrderByDescending(c => c.Id)
                .ProjectTo<LatestProductServiceModel>(this.mapper)
                .Take(3)
                .ToList();

        public ProductDetailsServiceModel Details(int id)
            => this.data
            .Products
            .Where(p => p.Id == id)
            .ProjectTo<ProductDetailsServiceModel>(this.mapper)
            .FirstOrDefault();

        public int Create(string brand, string name, string description, string imageUrl, int quantity, decimal price, int categoryId, int dealerId)
        {
            var productData = new Product
            {
                Brand = brand,
                Name = name,
                Description = description,
                ImageUrl = imageUrl,
                Quantity = quantity,
                Price = price,
                CategoryId = categoryId,
                DealerId = dealerId,
                IsPublic = false
            };

            this.data.Products.Add(productData);
            this.data.SaveChanges();

            return productData.Id;
        }

        public bool Edit(
            int id, 
            string brand, 
            string name, 
            string description, 
            string imageUrl, 
            int quantity, 
            decimal price, 
            int categoryId,
            bool isPublic)
        {
            var productData= this.data.Products.Find(id);

            if(productData==null)
            {
                return false;
            }

            productData.Brand = brand;
            productData.Name = name;
            productData.Description = description;
            productData.ImageUrl = imageUrl;
            productData.Quantity = quantity;
            productData.Price = price;
            productData.CategoryId = categoryId;
            productData.IsPublic = isPublic;

            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<ProductServiceModel> ByUser(string userId)
            => GetProducts(this.data
                .Products
                .Where(p => p.Dealer.UserId == userId));

        public bool IsByDealer(int productId, int dealerId)
            => this.data
                .Products
                .Any(p => p.Id == productId && p.DealerId == dealerId);

        public void ChangeVisility(int productId)
        {
            var product = this.data.Products.Find(productId);

            product.IsPublic = !product.IsPublic;

            this.data.SaveChanges();
        }

        public bool Delete(int productId)
        {
            var product = this.data.Products.Find(productId);

            this.data.Remove(product);

            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<string> AllBrands()
         => this.data
                .Products
                .Select(c => c.Brand)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

        public IEnumerable<ProductCategoryServiceModel> AllCategories()
            => this.data
            .Categories
            .ProjectTo<ProductCategoryServiceModel>(this.mapper)
            .ToList();

        public bool CategoryExists(int categoryId)
             => this.data
                .Categories
                .Any(c => c.Id == categoryId);

        private IEnumerable<ProductServiceModel> GetProducts(IQueryable<Product> productQuery)
            => productQuery
                .ProjectTo<ProductServiceModel>(this.mapper)
                .ToList();
    }
}
