namespace ShoesStore.Models
{
    public class CartProduct
    {
        public int Id { get; set; }
        public Product product {  get; set; }
        public int productId { get; set; }  
        public int Quantity { get; set; }

         public Cart cart { get; set; }
        public int CartId { get; set; }


    }
}
