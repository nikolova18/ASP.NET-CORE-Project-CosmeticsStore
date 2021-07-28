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

        ProductDetailsServiceModel Details(int id);

        int Create(
            string brand,
            string name,
            string description,
            string imageUrl,
            int quantity,
            decimal price,
            int categoryId,
            int dealerId);

        bool Edit(
            int productId,
            string brand,
            string name,
            string description,
            string imageUrl,
            int quantity,
            decimal price,
            int categoryId);

        IEnumerable<ProductServiceModel> ByUser(string userId);

        bool IsByDealer(int productId, int dealerId);

        IEnumerable<string> AllBrands();

        IEnumerable<ProductCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);
    }
}
