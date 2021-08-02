namespace CosmeticsStore.Controllers
{
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Mvc;
    using CosmeticsStore.Data;
    using CosmeticsStore.Models.Home;
    using CosmeticsStore.Services.Statistics;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly ApplicationDbContext data;
        private readonly IConfigurationProvider mapper;

        public HomeController(
            IStatisticsService statistics,
            ApplicationDbContext data,
            IMapper mapper)
        {
            this.statistics = statistics;
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public IActionResult Index() 
        {
            var products = this.data
                .Products
                .OrderByDescending(p => p.Id)
                .ProjectTo<ProductIndexViewModel>(this.mapper)
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

        public IActionResult Error() => View();

    }
}
