namespace CosmeticsStore.Infrastructure.Extensions
{
    using CosmeticsStore.Services.Product.Models;

    public static class ProductExtensions
    {
        public static string GetInformation(this IProductModel product)
       => product.Brand + "-" + product.Name + "-" + product.Quantity;
    }
}
