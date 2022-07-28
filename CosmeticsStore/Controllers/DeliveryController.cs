namespace CosmeticsStore.Controllers
{
    using CosmeticsStore.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DeliveryController : Controller
    {

        private readonly ApplicationDbContext data;

        public DeliveryController(ApplicationDbContext data) => this.data = data;

        [Authorize]
        public IActionResult Buy() => View();


    }
}
