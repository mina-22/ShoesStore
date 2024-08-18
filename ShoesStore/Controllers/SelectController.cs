using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoesStore.Data;
using ShoesStore.Models;
using ShoesStore.Models.ViewModels;

namespace ShoesStore.Controllers
{
    [Authorize]
    public class SelectController(ApplicationDbContext _db , UserManager<AppUser> _userManager) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }   
         public IActionResult selectedGender(bool gender)
        {
            ProductsCollection productsCollection = new ProductsCollection();
            productsCollection.Gender = gender;
            return RedirectToAction("GetNextGender", productsCollection);

        }
        public IActionResult GetNextProducts(int CollectionNum)
        {
            int count = _db.Products.Count();
            if (CollectionNum * 6 >= count)
                CollectionNum = 0;
            ProductsCollection productsCollection = new ProductsCollection();
            productsCollection.products = _db.Products.Skip(CollectionNum * 6).Take(6).ToList(); 
            productsCollection.CollectionNum = CollectionNum;
            return View("Collection", productsCollection);
        }
        public IActionResult GetPreviousProducts(int  CollectionNum)
        {
            int count = _db.Products.Count();
            ProductsCollection productsCollection = new ProductsCollection();
            if (CollectionNum < 0 )
            {
                CollectionNum =(int) Math.Ceiling(count / 6.0) - 1;
            }
            
            
            productsCollection.products = _db.Products.Skip(CollectionNum * 6).Take(6).ToList();
            productsCollection.CollectionNum = CollectionNum;

            return View("Collection", productsCollection);
        }
        public IActionResult GetNextGender(ProductsCollection productsCollection)
        {
           List<Product> products = _db.Products.Where(m=>m.Gender == productsCollection.Gender).ToList();
			if (productsCollection.CollectionNum * 6 >= products.Count())
				productsCollection.CollectionNum = 0;

            productsCollection.products = products.Skip(productsCollection.CollectionNum * 6).Take(6).ToList();
            if(productsCollection.Gender)
			return View("Men", productsCollection);
            else
				return View("Women", productsCollection);
		}
        public IActionResult GetPreviousGender(ProductsCollection productsCollection)
        {
            List<Product> products = _db.Products.Where(m => m.Gender == productsCollection.Gender).ToList();
            if(productsCollection.CollectionNum <0)
                productsCollection.CollectionNum = (int)Math.Ceiling(products.Count() / 6.0) - 1;
            productsCollection.products = products.Skip(productsCollection.CollectionNum * 6).Take(6).ToList();
            if (productsCollection.Gender)
                return View("Men", productsCollection);
            else
                return View("Women", productsCollection);
        }
        public IActionResult GetNextSale(int CollectionNum)
        {

            List<Product> products = _db.Products.Where(m => m.Sale>0).ToList();
            if (CollectionNum * 6 >= products.Count())
                CollectionNum = 0;
            ProductsCollection productsCollection = new ProductsCollection();
            productsCollection.products = products.Skip(CollectionNum * 6).Take(6).ToList();
            productsCollection.CollectionNum = CollectionNum;

            return View("Sale", productsCollection);
        }
        public IActionResult GetPreviousSale(int CollectionNum)
        {
            List<Product> products = _db.Products.Where(m => m.Sale > 0).ToList();
            if (CollectionNum < 0)
            {
                CollectionNum = (int)Math.Ceiling(products.Count() / 6.0) - 1;
            }
            ProductsCollection productsCollection = new ProductsCollection();

            productsCollection.products = products.Skip(CollectionNum * 6).Take(6).ToList();
            productsCollection.CollectionNum = CollectionNum;

            return View("Sale", productsCollection);
        }

       
    }
}
