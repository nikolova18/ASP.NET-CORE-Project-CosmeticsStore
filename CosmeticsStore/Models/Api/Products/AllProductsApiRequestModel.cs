namespace CosmeticsStore.Models.Api.Products
{
    public class AllProductsApiRequestModel
    {
        public string Brand { get; init; }

        public string SearchTerm { get; init; }

        public ProductSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int ProductsPerPage { get; init; } = 10;
    }
}
