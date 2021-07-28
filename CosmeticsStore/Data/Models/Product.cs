namespace CosmeticsStore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Product;

    public class Product
    {
        public int Id { get; init; }

        [Required]
        [StringLength(BrandMaxLength, MinimumLength = MinLength)]
        public string Brand { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = MinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(
            DescriptionMaxLength,
            MinimumLength = MinLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [Range(QuantityMin, QuantityMax)]
        public int Quantity { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "9999.99")]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; init; }

        public int DealerId { get; init; }

        public Dealer Dealer { get; init; }

    }
}
