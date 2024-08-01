using System.ComponentModel.DataAnnotations;

namespace ShoesStore.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        
        public string? UserEmail { get; set; }
        [Required(ErrorMessage ="Name is Requiered")]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
