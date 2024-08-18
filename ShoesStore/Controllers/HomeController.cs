using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoesStore.Data;
using ShoesStore.Models;
using ShoesStore.Models.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace ShoesStore.Controllers
{
	public class HomeController(ApplicationDbContext _db , UserManager<AppUser> _userManager, ILogger<HomeController> _logger) : Controller
	{
			
        public IActionResult Index()
		{
			List<Product> Products = _db.Products.ToList();
			return View(Products);
		}
		public IActionResult Cart()
		{
			var userId = _userManager.GetUserId(User);
			var user = _db.Users.Include(m=>m.cart).SingleOrDefault(m=>m.Id == userId);
			List<CartProduct> cartProducts = _db.cartProduct.Include(m=>m.product).Where(  m=>m.CartId==user.cart.Id).ToList();
            List<QuantityProduct> Qproducts = new List<QuantityProduct>();
			foreach(var item in cartProducts)
			{
				Qproducts.Add(new QuantityProduct
				{
					product = item.product,
					Quantity = item.Quantity,
					ItemId = item.Id
					
				}) ;

            }
            return View(Qproducts);
		}
		public IActionResult DeleteFromCart(int id)
		{
			var cartprod = _db.cartProduct.Find(id);
			_db.cartProduct.Remove(cartprod);
			_db.SaveChanges();
			return RedirectToAction("Cart");
		}
		//handel duplecate elements
		[HttpPost]
		[Authorize(Roles = Rules.RuleUser)]
        public IActionResult AddToCart(QuantityProduct Qproduct)
		{
			var userId = _userManager.GetUserId(User);
			var user = _db.Users.Include(m => m.cart).SingleOrDefault(m => m.Id == userId);

			var prod = _db.Products.Find(Qproduct.product.Id);
          List<  CartProduct >ProductCard = _db.cartProduct.Where(m => m.productId == Qproduct.product.Id && m.CartId == user.cart.Id).ToList();
			
			if (ProductCard.Count != 0) 

            {
				ProductCard[0].Quantity = Math.Min( Math.Max(Qproduct.Quantity +  ProductCard[0].Quantity, 1) ,50);
				_db.cartProduct.Update(ProductCard[0]);
				_db.SaveChanges();
				return RedirectToAction("Cart");
			}
			else
			{
                CartProduct cartProduct = new CartProduct();
                cartProduct.Quantity =Math.Min( Math.Abs(Qproduct.Quantity) ,50);
                cartProduct.CartId = user.cart.Id;
                cartProduct.productId = Qproduct.product.Id;

                _db.cartProduct.Add(cartProduct);
            }
			
			_db.SaveChanges();
			Qproduct.product = prod;
			return RedirectToAction("Cart");
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
			var prod = _db.Products.Find(id);
            var userId = _userManager.GetUserId(User);
			var  mina = _db.favorites.FirstOrDefault(m => m.ProductId == id && userId == m.UserId) ;


			QuantityProduct cartProduct = new QuantityProduct
			{
				product = prod,
				IsFavorite = false,
				FavId = 0
            };
			// ----------------------------------------
			if(mina  != null)
			{
				cartProduct.IsFavorite = true;
				cartProduct.FavId = mina.Id;
			}
			// ----------------------------------------
                return View(cartProduct);
		}
		public IActionResult LookBook()
		{
			return View();
		}
		public IActionResult OurStory()
		{
			return View();
		}
        [HttpPost]
        public IActionResult RemoveFav([FromBody] FavUser fav)
        {
            var Fav = _db.favorites.Find(fav.Id);
            _db.Remove(Fav);
            _db.SaveChanges();

            return Ok();
        }
        [HttpPost]

        public IActionResult AddFav([FromBody] FavUser fav)
        {
            Favorite Fav = new Favorite
            {
                UserId = _userManager.GetUserId(User),
                ProductId = fav.ProductId,

            };
            _db.favorites.Add(Fav);
            _db.SaveChanges();
            return Ok(Fav.Id);
        }
		public IActionResult ViewFavorite()
		{
			var UserId = _userManager.GetUserId(User);
			var favorites = _db.favorites.Where(m => m.UserId == UserId).Include(m=>m.product).ToList();
		
			return View(favorites);
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
