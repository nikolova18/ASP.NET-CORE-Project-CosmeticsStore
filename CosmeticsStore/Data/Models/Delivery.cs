namespace CosmeticsStore.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Delivery;

    public class Delivery
    {
        public int Id { get; init; }

        [Required]
        [Range(RecipientMinLength, RecipientMaxLength)]
        public string RecipientName { get; set; }

        [Required]
        [MaxLength(AddressMaxLenght)]
        public string DeliveryAddress { get; set; }

        [Required]
        [MaxLength(PhoneMaxLength)]
        public string DeliveryPhoneNumber { get; set; }

        [Required]
        public decimal Total { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
