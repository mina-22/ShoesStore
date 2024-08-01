using System.ComponentModel.DataAnnotations;

namespace ShoesStore.Models
{
	public class Cart
	{
		[Key]
	        public	int Id { get; set; }
		    
		    public List<Product> ?Products = new List<Product>();
	}
}
