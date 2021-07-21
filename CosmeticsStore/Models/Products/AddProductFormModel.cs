namespace CosmeticsStore.Models.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Product;

    public class AddProductFormModel
    {
        [Required]
        [StringLength(BrandMaxLength, MinimumLength = MinLength)]
        public string Brand { get; init; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = MinLength)]
        public string Name { get; init; }

        [Required]
        [StringLength(
            DescriptionMaxLength,
            MinimumLength = MinLength,
            ErrorMessage = "The field Description must minimum {2} chars long.")]
        public string Description { get; init; }

        [Url]
        [Required]
        [Display(Name="Image URL")]
        public string ImageUrl { get; init; }

        [Required]
        [Display(Name = "Quantity in mL")]
        [Range(QuantityMin,QuantityMax)]
        public int Quantity { get; init; }

        [Required]
        [Display(Name = "Price in BGN")]
        [Range(typeof(decimal), "0", "9999.99",
            ErrorMessage = "The price must be greater than 0.")]
        public decimal Price { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<ProductCategoryViewModel> Categories { get; set; }
    }
}
