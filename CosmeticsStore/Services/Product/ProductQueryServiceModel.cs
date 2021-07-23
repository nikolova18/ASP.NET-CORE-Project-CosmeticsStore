namespace CosmeticsStore.Services.Product
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int ProductsPerPage { get; init; }

        public int TotalProducts { get; init; }

        public IEnumerable<ProductServiceModel> Products { get; init; }
    }
}
