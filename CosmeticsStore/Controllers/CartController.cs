namespace CosmeticsStore.Controllers
{
    using CosmeticsStore.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CartController : Controller
    {

        private readonly ApplicationDbContext data;

        public CartController(ApplicationDbContext data) => this.data = data;

        [Authorize]
        public IActionResult All() => View();

       
    }
}
