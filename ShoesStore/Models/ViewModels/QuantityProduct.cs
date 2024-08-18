namespace ShoesStore.Models.ViewModels
{
    public class QuantityProduct
    {
        public int Quantity {  get; set; }
        public Product product { get; set; }
        public int ItemId { get; set; }
        public bool IsFavorite { get; set; }

        public int FavId { get; set; }
    }

}
