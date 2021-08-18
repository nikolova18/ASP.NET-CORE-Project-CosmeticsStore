namespace CosmeticsStore.Areas.Admin.Controllers
{
    using CosmeticsStore.Services.Product;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : AdminController
    {
        private readonly IProductService products;

        public ProductsController(IProductService products) => this.products = products;

        public IActionResult All()
        {
            var products = this.products
                .All(publicOnly: false)
                .Products;

            return View(products);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.products.ChangeVisility(id);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(int id)
        {
            this.products.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}