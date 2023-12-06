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
using MadeinHeavenBookStore.Models.Review;
using System.Net.Http;
using MadeinHeavenBookStore.Models.MVCService;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;

namespace MadeinHeavenBookStore.Controllers.Services
{
    public class ShopAPIService
	{
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MadeinHeavenBookStoreContext _context;
        private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
        private IHttpContextAccessor _httpContextAccessor;
        private WishlistService _wishlistService;

        public ShopAPIService(IHttpClientFactory httpClientFactory,
            MadeinHeavenBookStoreContext context,
            UserManager<MadeinHeavenBookStoreUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            WishlistService wishlistService)
        {
            _httpClientFactory = httpClientFactory;
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _wishlistService = wishlistService;
        }

        public async Task<List<Product>> ShopProduct(string? cate, string? search, int max, int min)
        {

            var httpClient = _httpClientFactory.CreateClient();
			httpClient.BaseAddress = new Uri("https://localhost:7074/");
			return await httpClient.GetFromJsonAsync<List<Product>>($"api/ShopAPI/ShopProduct?cate={cate}&search={search}&max={max}&min={min}");
        }

		public async Task<int> GetAllProducts()
		{
			var products = _context.Products.ToList();
			return products.Count();
		}


		public async Task<List<Product>> GetPagedProducts(string? category, string? search, int max, int min, int currentPage, int itemsPerPage)
		{
            // Assuming you have a method to retrieve all products and then filter based on category and search

            var httpClient = _httpClientFactory.CreateClient();
			httpClient.BaseAddress = new Uri("https://localhost:7074/");
			return await httpClient.GetFromJsonAsync<List<Product>>($"api/ShopAPI/GetPagedProducts?category={category}&search={search}&max={max}&min={min}&currentPage={currentPage}&itemsPerPage={itemsPerPage}");
        }

		public async Task<List<Category>> GetCategories()
		{
			List<Category> categories = _context.Categories.ToList();
			return categories;
		}


		public async Task AddToCart(int id)
		{
			var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var httpClient = _httpClientFactory.CreateClient();
			httpClient.BaseAddress = new Uri("https://localhost:7074/");
			var response = await httpClient.PostAsync($"api/ShopAPI/AddToCart/{id}/{userId}", new StringContent("", Encoding.UTF8, "application/json"));
		}



		public async Task AddtoWishlist(int id)
		{
			var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var httpClient = _httpClientFactory.CreateClient();
			httpClient.BaseAddress = new Uri("https://localhost:7074/");
			var response = await httpClient.PostAsync($"api/ShopAPI/AddtoWishlist/{id}/{userId}", new StringContent("", Encoding.UTF8, "application/json"));

		}

        public async Task AddProduct()
        {
            var httpClient = _httpClientFactory.CreateClient();
			httpClient.BaseAddress = new Uri("https://localhost:7074/");
			var response = await httpClient.PostAsync($"api/ShopAPI/AddProduct", new StringContent("", Encoding.UTF8, "application/json"));
        }




    }
}
