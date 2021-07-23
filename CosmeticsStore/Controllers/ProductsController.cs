namespace CosmeticsStore.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authorization;
    using CosmeticsStore.Data;
    using CosmeticsStore.Data.Models;
    using CosmeticsStore.Infrastructure;
    using CosmeticsStore.Models;
    using CosmeticsStore.Models.Products;
    using CosmeticsStore.Services.Product;

    public class ProductsController : Controller
    {
        private readonly IProductService products;
        private readonly ApplicationDbContext data;

        public ProductsController(ApplicationDbContext data, IProductService products)
        {
            this.products = products;
            this.data = data;
        }

        public IActionResult All([FromQuery]AllProductsQueryModel query)
        {
            var queryResult = this.products.All(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllProductsQueryModel.ProductPerPage);

            var productBrands = this.products.AllProductBrands();

            query.TotalProducts = queryResult.TotalProducts;
            query.Brands = productBrands;
            query.Products = queryResult.Products ;

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
