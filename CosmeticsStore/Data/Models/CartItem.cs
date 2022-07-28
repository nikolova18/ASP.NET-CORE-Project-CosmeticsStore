using System.ComponentModel.DataAnnotations;

namespace CosmeticsStore.Data.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
