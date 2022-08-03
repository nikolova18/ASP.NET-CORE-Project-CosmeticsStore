namespace CosmeticsStore.Models.Products
{
    using CosmeticsStore.Services.Product.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllProductsQueryModel
    {
        public const int ProductPerPage = 9;
        public string Brand { get; init; }

        [Display(Name ="Търси по текст:")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public ProductSorting Sorting { get; init; }

        public int TotalProducts { get; set; }

        public IEnumerable<string> Brands { get; set; }

        [Display(Name = "Категория")]
        public string CategoryName{ get; init; }

        public IEnumerable<ProductCategoryServiceModel> Categories { get; set; }

        public IEnumerable<ProductServiceModel> Products { get; set; }
        
    }
}
