namespace CosmeticsStore.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using CosmeticsStore.Data;
    using CosmeticsStore.Data.Models;
    using CosmeticsStore.Infrastructure;
    using CosmeticsStore.Models.Dealers;


    public class DealersController : Controller
    {
        private readonly ApplicationDbContext data;

        public DealersController(ApplicationDbContext data)
            =>this.data = data;


        [Authorize]
        public IActionResult Become()=> View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeDealerFormModel dealer)
        {
            var userId = this.User.Id();

            var userIdAlreadyDealer = this.data
                .Dealers
                .Any(d => d.UserId == userId);

            if (userIdAlreadyDealer)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(dealer);
            }

            var dealerData = new Dealer
            {
                Name = dealer.Name,
                PhoneNumber = dealer.PhoneNumber,
                UserId = userId
            };

            this.data.Dealers.Add(dealerData);
            this.data.SaveChanges();

            return RedirectToAction("All", "Products");
        }
    }
}
