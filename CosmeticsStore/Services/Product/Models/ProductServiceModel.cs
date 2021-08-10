namespace CosmeticsStore.Services.Product.Models
{
    public class ProductServiceModel : IProductModel
    {
        public int Id { get; init; }

        public string Brand { get; init; }

        public string Name { get; init; }

        public string ImageUrl { get; init; }

        public int Quantity { get; init; }

        public decimal Price { get; init; }

        public string CategoryName { get; init; }

        public bool IsPublic { get; init; }
    }
}
