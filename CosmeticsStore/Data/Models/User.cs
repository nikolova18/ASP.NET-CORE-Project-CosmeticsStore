namespace CosmeticsStore.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    using static DataConstants.User;

    public class User : IdentityUser
    {
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }

        public IEnumerable<Product> Products { get; init; } = new List<Product>();
        public IEnumerable<Delivery> Deliveries { get; init; } = new List<Delivery>();
    }
}
