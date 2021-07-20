
namespace CosmeticsStore.Models.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllProductsQueryModel
    {
        public const int ProductPerPage = 3;
        public string Brand { get; init; }
        public IEnumerable<string> Brands { get; set; }

        [Display(Name ="Search by text:")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalProducts { get; set; }

        public ProductSorting Sorting { get; init; }

        public IEnumerable<ProductListingViewModel> Products { get; set; }

    }
}
