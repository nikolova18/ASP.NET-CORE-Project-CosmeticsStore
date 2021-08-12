namespace CosmeticsStore.Test.Data
{
    using System.Linq;
    using CosmeticsStore.Data.Models;
    using System.Collections.Generic;

    public class Products
    {
        public static IEnumerable<Product> TenPublicCars
            => Enumerable.Range(0, 10).Select(i => new Product
            {
                IsPublic = true
            });
    }
}