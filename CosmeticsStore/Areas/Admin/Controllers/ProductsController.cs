namespace CosmeticsStore.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : AdminController
    {
        public IActionResult Index() => View();
    }
}