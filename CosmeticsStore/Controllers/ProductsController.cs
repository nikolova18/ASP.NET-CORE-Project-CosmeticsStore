namespace CosmeticsStore.Controllers
{
    using CosmeticsStore.Infrastructure;
    using CosmeticsStore.Models.Products;
    using CosmeticsStore.Services.Dealer;
    using CosmeticsStore.Services.Product;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : Controller
    {
        private readonly IProductService products;
        private readonly IDealerService dealer;

        public ProductsController( 
            IProductService products, 
            IDealerService dealer)
        {
            this.products = products;
            this.dealer = dealer; 
        }

        public IActionResult All([FromQuery]AllProductsQueryModel query)
        {
            var queryResult = this.products.All(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllProductsQueryModel.ProductPerPage);

            var productBrands = this.products.AllBrands();

            query.TotalProducts = queryResult.TotalProducts;
            query.Brands = productBrands;
            query.Products = queryResult.Products ;

            return View(query);
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myProducts = this.products.ByUser(this.User.Id());

            return View(myProducts);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.dealer.IsDealer(this.User.Id()))
            {
                return RedirectToAction(nameof(DealersController.Become),"Dealers");
            }

            return View(new ProductFormModel
            {
                Categories = this.products.AllCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(ProductFormModel product)
        {
            var dealerId = this.dealer.IdByUser(this.User.Id());

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (!this.products.CategoryExists(product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if(!ModelState.IsValid)
            {
                product.Categories = this.products.AllCategories();

                return View(product);
            }

            this.products.Create(
                product.Brand,
                product.Name,
                product.Description,
                product.ImageUrl,
                product.Quantity,
                product.Price,
                product.CategoryId,
                dealerId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.dealer.IsDealer(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            var product = this.products.Details(id);

            if (product.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(new ProductFormModel
            {
                Brand=product.Brand,
                Name=product.Name,
                Description=product.Description,
                ImageUrl=product.ImageUrl,
                Quantity=product.Quantity,
                Price=product.Price,
                CategoryId=product.CategoryId,
                Categories=this.products.AllCategories()
            });

        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id,ProductFormModel product)
        {
            var dealerId = this.dealer.IdByUser(this.User.Id());

            if (dealerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (!this.products.CategoryExists(product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.products.AllCategories();

                return View(product);
            }

            if (!this.products.IsByDealer(id, dealerId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.products.Edit(
               id,
               product.Brand,
               product.Name,
               product.Description,
               product.ImageUrl,
               product.Quantity,
               product.Price,
               product.CategoryId);

            return RedirectToAction(nameof(All));
        }
    }
}
