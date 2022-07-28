namespace CosmeticsStore.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Delivery;

    public class Delivery
    {
        public int Id { get; init; }

        public int CartId { get; set; }

        [Required]
        [Range(RecipientMinLength, RecipientMaxLength)]
        public string RecipientName { get; set; }

        [Required]
        [MaxLength(AddressMaxLenght)]
        public string DeliveryAddress { get; set; }

        [Required]
        [MaxLength(PhoneMaxLength)]
        public string DeliveryPhoneNumber { get; set; }
    }
}
