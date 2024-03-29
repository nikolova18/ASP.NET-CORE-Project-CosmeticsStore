﻿namespace CosmeticsStore.Services.Product.Models
{
    using System.Collections.Generic;

    public class ProductQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int ProductsPerPage { get; init; }

        public int TotalProducts { get; init; }

        public IEnumerable<ProductCategoryServiceModel> Categories { get; init; }


        public IEnumerable<ProductServiceModel> Products { get; init; }
    }
}
