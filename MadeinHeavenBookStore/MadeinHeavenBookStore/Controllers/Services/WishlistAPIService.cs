using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models;
using MadeinHeavenBookStore.Models.MVCService;
using MadeinHeavenBookStore.Models.ShipandCoupon;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace MadeinHeavenBookStore.Controllers.Services
{
	public class WishlistAPIService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly MadeinHeavenBookStoreContext _context;
		private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
		private IHttpContextAccessor _httpContextAccessor;
		private WishlistService _wishlistService;

		public WishlistAPIService(IHttpClientFactory httpClientFactory, 
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
		public async Task<List<Wishlist>> GetWishList()
		{
			var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			var httpClient = _httpClientFactory.CreateClient();
			httpClient.BaseAddress = new Uri("https://localhost:7074/");
			return await httpClient.GetFromJsonAsync<List<Wishlist>>($"api/WishlistAPI?idUser={userId}");
		}

		public async Task DeleteFromWishlist(int id)
		{
            var httpClient = _httpClientFactory.CreateClient();
			httpClient.BaseAddress = new Uri("https://localhost:7074/");
			await httpClient.DeleteAsync($"api/WishlistAPI/RemoveFromWishlist/{id}");
        }

	}
}
