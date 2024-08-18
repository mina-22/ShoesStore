using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.StaticFiles;
using Mono.TextTemplating;
using ShoesStore.Data;
using ShoesStore.Models;
using System.ComponentModel;
namespace ShoesStore.Controllers.Dashboard
{
    public class DashboardController (ApplicationDbContext _db ,IWebHostEnvironment hosting): Controller
    {
        [Authorize(Roles = Rules.RuleAdmin)]
        public IActionResult Index()
        {
            return View();
        }
		[Authorize(Roles = Rules.RuleAdmin)]

		public IActionResult RmoveImage(Product prod)
        {
            if (!string.IsNullOrEmpty(prod.PathImg))
            {
                string imgPath = Path.Combine(hosting.WebRootPath, "Images", prod.PathImg);
                if (System.IO.File.Exists(imgPath))
                {
                    System.IO.File.Delete(imgPath);
                }
            }
            return Ok();
        }
		[Authorize(Roles = Rules.RuleAdmin)]

		public bool  AddImage(Product product  , Product prod)
        {
            if (product.ProductImage != null)
            {
                string imgfolder = Path.Combine(hosting.WebRootPath, "Images");
                string imgpath = Path.Combine(imgfolder, product.ProductImage.FileName);

                if(System.IO.File.Exists(imgpath))
                {
                    return false;
                }
                else
                {
                  product.ProductImage.CopyTo(new FileStream(imgpath, FileMode.Create));
                  prod.PathImg = product.ProductImage.FileName;
                }
               
            }
            return true;
        }
        [Authorize(Roles = Rules.RuleAdmin)]


		public IActionResult GetProducts()
        {
            List<Product> products = _db.Products.ToList();

            return View(products);
        }
		[Authorize(Roles = Rules.RuleAdmin)]

		public IActionResult AddProduct(int id)
        {
           if(id != 0)
            {
                Product prod = _db.Products.Find(id);
                return View(prod);
            }
            else
              return View();
        }
		[Authorize(Roles = Rules.RuleAdmin)]

		[HttpPost]
		[Authorize(Roles = Rules.RuleAdmin)]

		public IActionResult AddProduct(Product product)
        {

            if(!ModelState.IsValid)
            {
                return View(product);
            }
            
           
            if(product.Id != 0)
            {
                Product prod = _db.Products.Find(product.Id);
                prod.Name = product.Name;
                prod.Description = product.Description;
                prod.Price = product.Price;
                prod.Sale = product.Sale;
                prod.Gender = product.Gender;
                RmoveImage(prod);

                if (AddImage(product, product) == false)
                {
                    TempData["ExistImage"] = "This File is Already Exist";
                    return RedirectToAction("AddProduct", product.Id);
                }
                _db.Products.Update(prod);
            }
            else
            {  
                if (AddImage(product, product)== false)
                {
                    TempData["ExistImage"] = "This File is Already Exist";

                    return RedirectToAction("AddProduct", product.Id);
                }
                _db.Products.Add(product);
            }
              

            _db.SaveChanges();
               return RedirectToAction("GetProducts");
        }

		[Authorize(Roles = Rules.RuleAdmin)]
		public IActionResult Delete(int id)
        {
            Product prod = _db.Products.FirstOrDefault(x => x.Id == id);
            if(prod == null)
            {
                return RedirectToAction("GetProducts");
            }
            // delete image from Folder Images
                  RmoveImage(prod);
            _db.Products.Remove(prod); _db.SaveChanges();
            return RedirectToAction("GetProducts");
        }
        [Authorize(Roles = Rules.RuleAdmin)]
        public IActionResult GetBlogs()
        {
            var blogs = _db.Blog.ToList();
            return View(blogs);
        }
        [Authorize(Roles = Rules.RuleAdmin)]
        public IActionResult DeleteBlog(int id)
        {

            var blog = _db.Blog.FirstOrDefault(m => m.Id == id);
            if(blog!=null)
            {
                _db.Blog.Remove(blog);
                _db.SaveChanges();
            }
            return RedirectToAction("GetBlogs");
        }
    }
}
