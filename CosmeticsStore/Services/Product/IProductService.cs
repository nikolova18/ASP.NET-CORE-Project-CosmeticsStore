namespace CosmeticsStore.Services.Product
{
    using CosmeticsStore.Models;
    using System.Collections.Generic;

    public interface IProductService
    {
        ProductQueryServiceModel All(
            string brand,
            string searchTerm,
            ProductSorting sorting,
            int currentPage,
            int productsPerPage);

        IEnumerable<string> AllProductBrands();
    }
}
