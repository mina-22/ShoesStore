using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace ShoesStore.Models
{
	public class AppUser : IdentityUser
	{
	
		public string? Address { set; get; }
        public	Cart? cart { set; get; }
		
    }
}
