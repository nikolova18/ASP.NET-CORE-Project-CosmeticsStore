namespace CosmeticsStore.Services.Product.Models
{
   public interface IProductModel
    {
        string Brand { get; }

        string Name { get; }

        int Quantity { get; }
    }
}
