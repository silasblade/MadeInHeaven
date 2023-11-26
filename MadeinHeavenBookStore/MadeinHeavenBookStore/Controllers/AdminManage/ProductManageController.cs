using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MadeinHeavenBookStore.Controllers.AdminManage
{
    public class ProductManageController : Controller
    {
        private readonly MadeinHeavenBookStoreContext _context;
        private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
        private IWebHostEnvironment Environment;

        public ProductManageController(MadeinHeavenBookStoreContext context, UserManager<MadeinHeavenBookStoreUser> userManager, IWebHostEnvironment _environment)
        {
            _context = context;
            _userManager = userManager;
            Environment = _environment;
        }
        public IActionResult ProductManage(string search, string catsearch)
        {
            List<Product> products = _context.Products
                .Include(c => c.Categories)
                .ToList();

            
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(c => c.NameProduct.Contains(search)).ToList();
            }

            if (!string.IsNullOrEmpty(catsearch))
            {
                Category catsear = _context.Categories.FirstOrDefault(c => c.Name == catsearch);
                products = products.Where(c => c.Categories.Contains(catsear)).ToList();
            }

            ViewBag.CatList = _context.Categories.ToList();

            return View(products);
        }

        public IActionResult AddProduct()
        {
            Product product = new Product();
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("ProductManage");
        }

        public IActionResult DeleteProduct()
        {
            Product product = new Product();
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("ProductManage");
        }

        public IActionResult EditProduct(int id)
        {
            Product product = _context.Products
                .Include (c => c.Categories)
                .FirstOrDefault(c => c.IdProduct == id);
            ViewBag.Categories = _context.Categories
                .ToList();
            return View(product);
        }

        [HttpPost]
        public IActionResult SaveProduct(Product product, IFormFile formFile, IFormFile formFile2, IFormFile formFile3)
        {
            string imagePath1 = "";
            string imagePath2 = "";
            string imagePath3 = "";
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;
            string path = Path.Combine(this.Environment.WebRootPath, "uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if(formFile != null)
            {
                string name = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                string filePath = Path.Combine(path, name);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }
                imagePath1 = "/uploads/" + name;
            }
            

            if(formFile2 != null)
            {
                string name2 = Guid.NewGuid().ToString() + Path.GetExtension(formFile2.FileName);
                string filePath2 = Path.Combine(path, name2);
                using (var stream = new FileStream(filePath2, FileMode.Create))
                {
                    formFile2.CopyTo(stream);
                }
                imagePath2 = "/uploads/" + name2;
            }
            

            if(formFile3 != null)
            {
                string name3 = Guid.NewGuid().ToString() + Path.GetExtension(formFile3.FileName);
                string filePath3 = Path.Combine(path, name3);
                using (var stream = new FileStream(filePath3, FileMode.Create))
                {
                    formFile3.CopyTo(stream);
                }
                imagePath3 = "/uploads/" + name3;
            }    
            

            Console.WriteLine(product.IdProduct);
            Console.WriteLine(product.NameProduct);  
            Product nPorudct = _context.Products.Find(product.IdProduct);
                    nPorudct.NameProduct = product.NameProduct;
                    nPorudct.Author = product.Author;
                    nPorudct.Publishing = product.Publishing;
                    nPorudct.Categories = product.Categories;
                    nPorudct.Price = product.Price;
                    nPorudct.Description = product.Description;
                    nPorudct.imageurl1 = string.IsNullOrWhiteSpace(imagePath1) ? nPorudct.imageurl1 : imagePath1;
                    nPorudct.imageurl2 = string.IsNullOrWhiteSpace(imagePath2) ? nPorudct.imageurl2 : imagePath2;
                    nPorudct.imageurl3 = string.IsNullOrWhiteSpace(imagePath3) ? nPorudct.imageurl3 : imagePath3;

            _context.SaveChanges();
           
            return RedirectToAction("ProductManage");
        }

        private string GetUniqueFileName(string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            string guid = Guid.NewGuid().ToString().Substring(0, 8); // Lấy 8 ký tự đầu của GUID

            return $"{name}_{guid}{extension}";
        }

        [HttpPost]
        public IActionResult AddNewCat(string catname, int idproduct)
        {
            if(catname == null)
            {
                Console.WriteLine("catname null");
            }
            Console.WriteLine(catname);

            if (idproduct == null)
            {
                Console.WriteLine("product null");
            }
            Console.WriteLine(idproduct);
            
            Category category = _context.Categories.Where(c => c.Name == catname).FirstOrDefault();
            Product product = _context.Products
                .Include(c => c.Categories)
                .FirstOrDefault(p => p.IdProduct == idproduct);


            Category catcheck = product.Categories.FirstOrDefault(c => c.Name == catname);
            if(catcheck != null)
            {
                return RedirectToAction("EditProduct", (new { id = idproduct }));
            }

            product.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("EditProduct", (new { id = idproduct }));
        }

        [HttpPost]   
        
        public IActionResult DeleteCat(string catname, int idproduct)
        { 

            Console.WriteLine("Hàm này được gọi");
            if (catname == null)
            {
                Console.WriteLine("catname null");
            }
            Console.WriteLine(catname);

            if (idproduct == null)
            {
                Console.WriteLine("product null");
            }
            Console.WriteLine(idproduct);

            Category category = _context.Categories.Where(c => c.Name == catname).FirstOrDefault();
            Product product = _context.Products
                .Include(c => c.Categories)
                .FirstOrDefault(p => p.IdProduct == idproduct);
            Console.WriteLine(category.Name);
            Console.WriteLine(product.NameProduct);
            product.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("EditProduct", (new { id = idproduct }));
        }


    }
}
