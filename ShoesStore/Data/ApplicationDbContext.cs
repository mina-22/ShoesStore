using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoesStore.Models;

namespace ShoesStore.Data
{
	public class ApplicationDbContext : IdentityDbContext<AppUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			:base(options)
		{

		}

	   public	DbSet<Product> Products { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Cart> cart { get; set; }
		public DbSet<CartProduct> cartProduct { get; set; }
	}
}
