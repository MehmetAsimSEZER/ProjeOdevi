namespace Presantation.Models
{
    public class CartVM
    {
        public List<CartItem>? CartItems { get; set; }

        public decimal GrandTotal { get; set; }
    }
}
