namespace CosmeticsStore.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CosmeticsStore.Services.Product;
    using CosmeticsStore.Services.Product.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public class HomeController : Controller
    {
        private readonly IProductService products;
        private readonly IMemoryCache cache;

        public HomeController(
            IProductService products,
            IMemoryCache cache)
        {
            this.products = products;
            this.cache = cache;
        }

        public IActionResult Index() 
        {
            const string latestProductsCacheKey = "LatestProductsCacheKey";

            var latestProducts = this.cache.Get<List<LatestProductServiceModel>>(latestProductsCacheKey);

            if (latestProducts == null)
            {
                latestProducts = this.products
                  .Latest()
                  .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(latestProductsCacheKey, latestProducts, cacheOptions);
            }

            return View(latestProducts);
        }

        public IActionResult Error() => View();
    }
}
