namespace CosmeticsStore.Controllers
{
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Mvc;
    using CosmeticsStore.Data;
    using CosmeticsStore.Models.Home;
    using CosmeticsStore.Services.Statistics;
    using CosmeticsStore.Services.Product;

    public class HomeController : Controller
    {
        private readonly IProductService products;
        private readonly IStatisticsService statistics;


        public HomeController(
            IProductService products,
            IStatisticsService statistics)
        {
            this.statistics = statistics;
            this.products = products;
        }

        public IActionResult Index() 
        {
            var latestproducts = this.products
                .Latest()
                .ToList();

            var totalStatistics = this.statistics.Total();


            return View(new IndexViewModel
            {
                TotalProducts = totalStatistics.TotalProducts,
                TotalUsers = totalStatistics.TotalUsers,
                Products = latestproducts
            }) ;
        }

        public IActionResult Error() => View();

    }
}
