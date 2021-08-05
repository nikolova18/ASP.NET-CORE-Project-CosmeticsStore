namespace CosmeticsStore.Models.Home
{
    using CosmeticsStore.Services.Product.Models;
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int TotalProducts { get; init; }
        public int TotalUsers { get; init; }
        public int TotalPurchase { get; init; }
        public List<LatestProductServiceModel> Products { get; init; }
    }
}
