using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesStore.Data;
using ShoesStore.Models;
using ShoesStore.Models.ViewModels;
using System.Diagnostics;

namespace ShoesStore.Controllers
{
	public class HomeController(ApplicationDbContext _db , UserManager<AppUser> _userManager) : Controller
	{
		private readonly ILogger<HomeController> _logger;
			
        public IActionResult Index()
		{
			List<Product> Products = _db.Products.ToList();
			return View(Products);
		}
		public IActionResult Cart()
		{
			return View();
		}
		public IActionResult AddToCart(Product product)
		{
			var userId = _userManager.GetUserId(User);

			var user = _db.Users.Include(m=>m.cart).SingleOrDefault(m => m.Id == userId);
			var prod = _db.Products.Find(product.Id);
			prod.Quantity = product.Quantity;
			user.cart.Products.Add(prod);
				
			_db.Users.Update(user);
			_db.SaveChanges();
			int i = 0;
			return View();
		}
        [Authorize]
        public IActionResult Contact()
		{
			
		
			Blog blog = new Blog();
			var userId = _userManager.GetUserId(User);
			
			var user = _db.Users.Find(userId);
			blog.UserEmail = user.Email;
			 return  View(blog); 
		}
		[HttpPost]
        [Authorize]
        public IActionResult AddBlog(Blog blog)
		{
			blog.UserEmail = _userManager.GetUserName(User);
			if(!ModelState.IsValid)
			{
				return View("Contact",blog);
			}
			_db.Blog.Add(blog);
			_db.SaveChanges();
            TempData["Addsucc"] = "Comment Added successfully";
            return RedirectToAction("Contact");
		}
        [Authorize]

        public IActionResult SingleProduct(int id)
		{
			var product = _db.Products.Find(id);
			return View(product);
		}
		public IActionResult LookBook()
		{
			return View();
		}
		public IActionResult OurStory()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
