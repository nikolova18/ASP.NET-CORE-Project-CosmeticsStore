namespace CosmeticsStore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Product;

    public class Product
    {
        public int Id { get; init; }

        [Required]
        [StringLength(BrandMaxLength, MinimumLength = MinLength)]
        public string Brand { get; init; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = MinLength)]
        public string Name { get; init; }

        [Required]
        [StringLength(
            DescriptionMaxLength,
            MinimumLength = MinLength)]
        public string Description { get; init; }

        [Required]
        public string ImageUrl { get; init; }

        [Required]
        [Range(QuantityMin, QuantityMax)]
        public int Quantity { get; init; }

        [Required]
        [Range(typeof(decimal), "0", "9999.99")]
        public decimal Price { get; init; }

        public int CategoryId { get; init; }

        public Category Category { get; init; }

        public int DealerId { get; init; }

        public Dealer Dealer { get; init; }

    }
}
