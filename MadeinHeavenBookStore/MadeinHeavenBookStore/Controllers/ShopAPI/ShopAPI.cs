using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using Microsoft.CodeAnalysis;
using System.Security.Claims;

namespace MadeinHeavenBookStore.Models.MVCService
{
    [Route("api/ShopAPI")]
    [ApiController]
    public class ShopAPI : ControllerBase
	{
        private readonly MadeinHeavenBookStoreContext _context;
        private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
		private IHttpContextAccessor _httpContextAccessor;

		public ShopAPI(MadeinHeavenBookStoreContext context, UserManager<MadeinHeavenBookStoreUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}

		//[HttpGet]
		//[Route("ShopProduct")]
  //      public async Task<List<Product>> ShopProduct(string? cate, string? search, int max, int min)
  //      {
        
		//	List<Product> products = _context.Products
		//		.Include(c => c.Categories)
		//		.ToList();

		//	if (!string.IsNullOrEmpty(cate))
  //          {
  //              Category cat = _context.Categories.FirstOrDefault(c=> c.Name.Equals(cate));
  //              if (cat != null)
  //              {
		//			products = products.Where(c => c.Categories.Contains(cat))
		//			.ToList();
		//		}
                
  //          }

  //          if(!string.IsNullOrEmpty(search))
  //          {
		//		products = products.Where(c => c.NameProduct.Contains(search)).ToList();
		//	}
		//	if(max > min && max != min)
		//	{
		//		if (max != null)
		//		{
		//			products = products.Where(c => c.Price < max).ToList();
		//		}
		//		if (min != null)
		//		{
		//			products = products.Where(c => c.Price > min).ToList();
		//		}
		//	}
  //          products.Reverse();



  //          return products;
  //      }

		public async Task<List<Product>> ShopProduct(string? cate, string? search, int max, int min)
		{
			var productsQuery = _context.Products.AsQueryable();

			if (!string.IsNullOrEmpty(cate))
			{
				Category cat = _context.Categories.FirstOrDefault(c => c.Name.Equals(cate));
				if (cat != null)
				{
					productsQuery = productsQuery.Where(c => c.Categories.Contains(cat));
				}
			}

			if (!string.IsNullOrEmpty(search))
			{
				productsQuery = productsQuery.Where(c => c.NameProduct.Contains(search));
			}

			if (max > min && max != min)
			{
				if (max != null)
				{
					productsQuery = productsQuery.Where(c => c.Price < max);
				}
				if (min != null)
				{
					productsQuery = productsQuery.Where(c => c.Price > min);
				}
			}

			var products = await productsQuery.ToListAsync();
			products.Reverse();

			return products;
		}

		public async Task<int> GetAllProducts()
		{
			var products = _context.Products.ToList();
			return products.Count();
		}

        [HttpGet]
        [Route("GetPagedProducts")]
        public async Task<ActionResult<List<Product>>> GetPagedProducts(string? category, string? search, int max, int min, int currentPage, int itemsPerPage)
		{
			// Assuming you have a method to retrieve all products and then filter based on category and search

			var allProducts = await ShopProduct(category, search, max, min);

			// Calculate the index to start retrieving products based on the current page
			var startIndex = (currentPage - 1) * itemsPerPage;

            // Retrieve the subset of products for the current page
            var pagedProducts = allProducts.Skip(startIndex).Take(itemsPerPage).ToList();

			return Ok(pagedProducts);
		}

		public async Task<List<Category>> GetCategories()
		{
			List<Category> categories = _context.Categories.ToList();
			return categories;
		}


		[HttpPost]
		[Route("AddToCart/{id}/{userId}")]
		public async Task<IActionResult> AddToCart(int id, string userId)
		{

			var shoppingCartCheck =  _context.ShopCarts
				.FirstOrDefault(c => c.Id == userId && c.IdProduct == id);
			if (shoppingCartCheck == null)
			{
				var product = _context.Products.Find(id);
				var shopcart = new ShopCart
				{
					Product = product,
					IdProduct = id,
					Id = userId
				};
				_context.ShopCarts.Add(shopcart);
				_context.SaveChanges();
			}
			else
			{
				shoppingCartCheck.Quantity++;
				_context.SaveChanges();
			}
			return Ok();
		}


		[HttpPost]
		[Route("AddtoWishlist/{id}/{userId}")]
		public async Task<IActionResult> AddtoWishlist(int id, string userId)
		{
			Product product = _context.Products.Find(id);
			Wishlist wishlistcheck = _context.Wishlists
				.FirstOrDefault(w => w.Product == product && w.IdUser == userId);
			if (wishlistcheck == null)
			{
				Wishlist wishlist = new Wishlist();
				wishlist.Product = product;
				wishlist.IdUser = userId;
				_context.Wishlists.Add(wishlist);
				_context.SaveChanges();
			}
			return Ok();
			
		}

        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct()
        {
            Product product = new Product();
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok();
        }






    }
}
