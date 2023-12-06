using MadeinHeavenBookStore.Areas.Identity.Data;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MadeinHeavenBookStore.Models.MVCService
{
    public class ProductService
    {
        private readonly MadeinHeavenBookStoreContext _context;
        private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
        private IWebHostEnvironment Environment;


        public ProductService(MadeinHeavenBookStoreContext context, UserManager<MadeinHeavenBookStoreUser> userManager, IWebHostEnvironment _environment)
        {
            _context = context;
            _userManager = userManager;
            Environment = _environment;
        }

        public async Task<Product> AddCategory(int id, string catname)
        {
            if(id != null &&  catname != null)
            {
                Console.WriteLine(catname + "Function Calala");
                Product p = _context.Products.Find(id);
                Category cat = _context.Categories
                    .FirstOrDefault(c => c.Name.Contains(catname));
                if (!p.Categories.Contains(cat))
                {
                    p.Categories.Add(cat);
                    _context.SaveChanges();
                }
            }
            
     
            return null;

        }
        public async Task <Product> DeleteCategory(int  id, string catname)
        {
            Product p = _context.Products.Find(id);
            Category cat = _context.Categories
                .FirstOrDefault(c => c.Name.Contains(catname));

            p.Categories.Remove(cat);
            _context.SaveChanges();
            return null;
        }
            

        public async Task<List<Category>> GetCategories()
        {
            List<Category> categories = _context.Categories.ToList();
            return categories;
        }

        public async Task <Product> GetAProduct(int id)
        {
            Product product = await _context.Products
                .Include(c => c.Categories)
                .FirstOrDefaultAsync(x => x.IdProduct == id);
            return product;
        }

        public async Task<List<Product>> GetProduct()
        {
            List<Product> products = _context.Products
                .Include(c => c.Categories)
                .ToList();
            products.Reverse();
            return products;
        }

        public async Task<List<Product>> GetPagedProducts(string category, int currentPage, int itemsPerPage)
        {
            // Assuming you have a method to retrieve all products and then filter based on category and search

            var allProducts = await GetProduct();

            // Calculate the index to start retrieving products based on the current page
            var startIndex = (currentPage - 1) * itemsPerPage;

            // Retrieve the subset of products for the current page
            var pagedProducts = allProducts.Skip(startIndex).Take(itemsPerPage).ToList();

            return pagedProducts;
        }

        public async Task <Product> DeleteProduct(int id)
        {
            Product product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return null;
        }

        public async Task<Product> AddProduct()
        {
            Product product = new Product();
            _context.Products.Add(product);
            _context.SaveChanges();
            return null;
        }



        public async Task<Product> SaveProduct(Product product, IBrowserFile formFile, IBrowserFile formFile2, IBrowserFile formFile3)
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

            if (formFile != null)
            {
                string name = Guid.NewGuid().ToString() + Path.GetExtension(formFile.Name);
                string filePath = Path.Combine(path, name);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.OpenReadStream(formFile.Size).CopyToAsync(stream);
                }
                imagePath1 = "/uploads/" + name;
            }


            if (formFile2 != null)
            {
                string name2 = Guid.NewGuid().ToString() + Path.GetExtension(formFile2.Name);
                string filePath2 = Path.Combine(path, name2);
                using (var stream = new FileStream(filePath2, FileMode.Create))
                {
                    await formFile2.OpenReadStream(formFile2.Size).CopyToAsync(stream);
                }
                imagePath2 = "/uploads/" + name2;
            }


            if (formFile3 != null)
            {
                string name3 = Guid.NewGuid().ToString() + Path.GetExtension(formFile3.Name);
                string filePath3 = Path.Combine(path, name3);
                using (var stream = new FileStream(filePath3, FileMode.Create))
                {
                    await formFile3.OpenReadStream(formFile3.Size).CopyToAsync(stream);
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

            return null;
        }

    }
}
