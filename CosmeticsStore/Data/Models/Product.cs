namespace CosmeticsStore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Product
    {
        public int Id { get; init; }

        [Required]
        [StringLength(ProductBrandMaxLength, MinimumLength = TextMinLength)]
        public string Brand { get; init; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = TextMinLength)]
        public string Name { get; init; }

        [Required]
        [StringLength(
            ProductDescriptionMaxLength,
            MinimumLength = TextMinLength)]
        public string Description { get; init; }

        [Required]
        public string ImageUrl { get; init; }

        [Required]
        [Range(ProductQuantityMin, ProductQuantityMax)]
        public int Quantity { get; init; }

        [Required]
        [Range(typeof(decimal), "0", "9999.99")]
        public decimal Price { get; init; }

        public int CategoryId { get; init; }

        public Category Category { get; init; }



    }
}
