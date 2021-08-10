namespace CosmeticsStore.Services.Product
{
    using CosmeticsStore.Models;
    using System.Collections.Generic;
    using CosmeticsStore.Services.Product.Models;

    public interface IProductService
    {
        ProductQueryServiceModel All(
            string brand = null,
            string searchTerm = null,
            ProductSorting sorting=ProductSorting.DateCreated,
            int currentPage=1,
            int productsPerPage=int.MaxValue,
            bool publicOnly = true);

        IEnumerable<LatestProductServiceModel> Latest();

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
            int categoryId,
            bool isPublic);

        IEnumerable<ProductServiceModel> ByUser(string userId);

        bool IsByDealer(int productId, int dealerId);

        void ChangeVisility(int productId);

        IEnumerable<string> AllBrands();

        IEnumerable<ProductCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);
    }
}