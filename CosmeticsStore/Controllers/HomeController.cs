namespace CosmeticsStore.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using CosmeticsStore.Data;
    using CosmeticsStore.Models;
    using CosmeticsStore.Models.Home;
    using CosmeticsStore.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly ApplicationDbContext data;

        public HomeController(
            IStatisticsService statistics,
            ApplicationDbContext data)
        {
            this.statistics = statistics;
            this.data = data;
        }

        public IActionResult Index() 
        {
            var products = this.data
                .Products
                .OrderByDescending(p => p.Id)
                .Select(p => new ProductIndexViewModel
                {
                    Id = p.Id,
                    Brand = p.Brand,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Quantity = p.Quantity,
                    Price = p.Price
                })
                .Take(3)
                .ToList();

            var totalStatistics = this.statistics.Total();


            return View(new IndexViewModel
            {
                TotalProducts = totalStatistics.TotalProducts,
                TotalUsers = totalStatistics.TotalUsers,
                Products = products
            }) ;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()=>View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
