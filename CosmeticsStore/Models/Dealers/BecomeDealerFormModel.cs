namespace CosmeticsStore.Models.Dealers
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Dealer;

    public class BecomeDealerFormModel
    {
        [Required]
        [StringLength(NameMaxLenght, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(PhoneMaxLength, MinimumLength = PhoneMinLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
