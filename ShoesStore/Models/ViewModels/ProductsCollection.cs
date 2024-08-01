namespace ShoesStore.Models.ViewModels
{
    public class ProductsCollection
    {
       public  List<Product> ?products { set; get; }
        public int CollectionNum { set; get; }
        public bool Gender { get; set; }
    }
}
