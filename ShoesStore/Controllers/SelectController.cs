using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoesStore.Data;
using ShoesStore.Models;
using ShoesStore.Models.ViewModels;

namespace ShoesStore.Controllers
{
    public class SelectController(ApplicationDbContext _db) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
         public IActionResult selectedGender(bool gender)
        {
            ProductsCollection productsCollection = new ProductsCollection();
            productsCollection.Gender = gender;
            return RedirectToAction("GetNextGender", productsCollection);

        }
        [Authorize]
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

        [Authorize]
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
        [Authorize]

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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
