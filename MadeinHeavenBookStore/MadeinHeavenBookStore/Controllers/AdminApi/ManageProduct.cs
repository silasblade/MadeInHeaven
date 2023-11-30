using MadeinHeavenBookStore.Models;
using MadeinHeavenBookStore.Models.MVCService;
using MadeinHeavenBookStore.Models.ShipandCoupon;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MadeinHeavenBookStore.Controllers.AdminApi
{
    [Route("api/manageproduct")]
    [ApiController]
    public class ManageProduct : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly MadeinHeavenBookStoreContext _context;
        public ManageProduct(ProductService productService, MadeinHeavenBookStoreContext context)
        {
            _productService = productService;
            _context = context;
        }

        [HttpGet("GetProDuct")]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = _context.Products
                .ToList();
            return Ok(products);
        }

        [HttpGet("GetProCat/{id}")]
        public async Task<ActionResult<List<Category>>> GetProCat(int id)
        {
            
            List<Category> ProCat = GetCategories(id);
            return Ok(ProCat);
        }

        public List<Category> GetCategories(int id) {

            Product product = _context.Products
                   .FirstOrDefault(x => x.IdProduct == id);
            List<Category> ProCat = product.Categories.ToList();
            return ProCat;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
                Product product = await _context.Products.FindAsync(id);
                _context.Products.Remove(product);
                _context.SaveChanges();
                return Ok("200");

        }

        [HttpPost]
        public async Task<ActionResult<List<Product>>> AddProduct()
        {
            Console.WriteLine("api serv được gọi");
            Product product = new Product();

             _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        [HttpPut]
        public async Task<ActionResult<List<Product>>> SaveProduct(List<Product> products)
        {
            foreach(Product pd in products)
            {
             
                _context.SaveChanges();
            }    
            return Ok();
        }
    }

    
}
