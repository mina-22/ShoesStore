using ShoesStore.Util;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoesStore.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }
		[Required]
		[MaxLength(70)]
		public string Description { get; set; }
		[Required]
		[Range(30, 150)]
		public float? Price { get; set; }
		[Required]
		[Range(0, 150)]
		[ValidationSale]
		public float Sale { get; set; }
		[Required]
		public bool Gender { get; set; }  // man => true

		public string? PathImg { get; set; }
		[NotMapped]
		[Required(ErrorMessage = "Product Image is Required")]
		public IFormFile ProductImage { set; get; }
		public List<Cart>? Carts { get; set; }
		
		public int ?Quantity{ get; set; }
	}
}
