namespace CosmeticsStore.Models.Products
{
    using CosmeticsStore.Services.Product;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllProductsQueryModel
    {
        public const int ProductPerPage = 3;
        public string Brand { get; init; }

        [Display(Name ="Search by text:")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public ProductSorting Sorting { get; init; }

        public int TotalProducts { get; set; }

        public IEnumerable<string> Brands { get; set; }

        public IEnumerable<ProductServiceModel> Products { get; set; }
    }
}
