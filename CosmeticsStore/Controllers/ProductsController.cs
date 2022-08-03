namespace CosmeticsStore.Controllers
{
    using AutoMapper;
    using CosmeticsStore.Infrastructure.Extensions;
    using CosmeticsStore.Models.Products;
    using CosmeticsStore.Services.Dealer;
    using CosmeticsStore.Services.Product;
    using CosmeticsStore.Services.Product.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;
    public class ProductsController : Controller
    {
        private readonly IProductService products;
        private readonly IDealerService dealers;
        private readonly IMapper mapper;

        public ProductsController( 
            IProductService products, 
            IDealerService dealers,
            IMapper mapper)
        {
            this.products = products;
            this.dealers = dealers;
            this.mapper = mapper;
        }
        public IActionResult Delete(int id)
        {
            this.products.Delete(id);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery]AllProductsQueryModel query)
        {
            var queryResult = this.products.All(
                query.Brand,
                query.CategoryName,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage, 
                AllProductsQueryModel.ProductPerPage);

            var productBrands = this.products.AllBrands();
            var categories = this.products.AllCategories();

            query.TotalProducts = queryResult.TotalProducts;
            query.Categories = categories;
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
        
        public IActionResult Details(int id, string information)
        {
            var product = this.products.Details(id);

            if (information != product.GetInformation())
            {
                return BadRequest();
            }
            return View(product);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.dealers.IsDealer(this.User.Id()))
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
            var dealerId = this.dealers.IdByUser(this.User.Id());

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

            var productId=this.products.Create(
                product.Brand,
                product.Name,
                product.Description,
                product.ImageUrl,
                product.Quantity,
                product.Price,
                product.CategoryId,
                dealerId);

            TempData[GlobalMessageKey] = "You product was added and is awaiting for approval!";

            return RedirectToAction(nameof(Details), new { id = productId, information = product.GetInformation() });
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.dealers.IsDealer(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            var product = this.products.Details(id);

            if (product.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var productForm = this.mapper.Map<ProductFormModel>(product);

            productForm.Categories = this.products.AllCategories();

            return View(productForm);

        }
        [Authorize]
        public IActionResult Cart()
        {
            return View();

        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id,ProductFormModel product)
        {
            var dealerId = this.dealers.IdByUser(this.User.Id());

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

            var edited = this.products.Edit(
               id,
               product.Brand,
               product.Name,
               product.Description,
               product.ImageUrl,
               product.Quantity,
               product.Price,
               product.CategoryId,
               this.User.IsAdmin());

            if (!edited)
            {
                return BadRequest();
            }

            TempData[GlobalMessageKey] = $"You product was edited{(this.User.IsAdmin() ? string.Empty : " and is awaiting for approval")}!";

            return RedirectToAction(nameof(Details), new { id, information = product.GetInformation() });
        }
    }
}
