using System.Collections.Generic;

namespace CosmeticsStore.Data.Models
{
    public class Cart
    {
        public int Id{ get; init; }

        public IEnumerable<CartItem> CartItems { get; init; } = new List<CartItem>();

        public string UserId { get; set; }
        public User User { get; set; }

        public decimal Total { get; set; }

    }
}
