namespace CosmeticsStore.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authorization;
    using CosmeticsStore.Data;
    using CosmeticsStore.Data.Models;
    using CosmeticsStore.Infrastructure;
    using CosmeticsStore.Models.Products;


    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext data;

        public ProductsController(ApplicationDbContext data)
            => this.data = data;

        public IActionResult All([FromQuery]AllProductsQueryModel query)
        {
            var productsQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Brand))
            {
                productsQuery = productsQuery.Where(p => p.Brand == query.Brand);
            }


            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                      (p.Brand+" "+p.Name).ToLower().Contains(query.SearchTerm.ToLower()) || 
                      p.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            productsQuery = query.Sorting switch
            {
                ProductSorting.Price => productsQuery.OrderBy(p => p.Price),
                ProductSorting.Quantity => productsQuery.OrderBy(p => p.Quantity),
                ProductSorting.BrandAndName => productsQuery.OrderBy(p => p.Brand).ThenBy(p=>p.Name),
                ProductSorting.DateCreated or _ => productsQuery.OrderByDescending(p => p.Id)
            };

            var totalProducts = productsQuery.Count();

            var products =productsQuery
                .Skip((query.CurrentPage-1)*AllProductsQueryModel.ProductPerPage)
                .Take(AllProductsQueryModel.ProductPerPage)
                .Select(p => new ProductListingViewModel
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

            var productBrands = this.data
                .Products
                .Select(p => p.Brand)
                .Distinct()
                .ToList();

            query.Brands = productBrands;
            query.Products = products;
            query.TotalProducts = totalProducts;

            return View(query);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsDealer())
            {
                return RedirectToAction(nameof(DealersController.Become),"Dealers");
            }

            return View(new AddProductFormModel
            {
                Categories = this.GetProductCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddProductFormModel product)
        {
            var dealerId = this.data
                .Dealers
                .Where(d => d.UserId == this.User.GetId())
                .Select(d => d.Id)
                .FirstOrDefault();

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (!this.data.Categories.Any(c=>c.Id==product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if(!ModelState.IsValid)
            {
                product.Categories = this.GetProductCategories();

                return View(product);
            }

            var productData = new Product
            {
                Brand = product.Brand,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Quantity = product.Quantity,
                Price = product.Price,
                CategoryId = product.CategoryId,
                DealerId = dealerId
            };

            this.data.Products.Add(productData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private bool UserIsDealer()
            => this.data
                .Dealers
                .Any(d => d.UserId == this.User.GetId());

        private IEnumerable<ProductCategoryViewModel> GetProductCategories()
            => this.data
            .Categories
            .Select(c => new ProductCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();
    }
}
