using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models;
using MadeinHeavenBookStore.Models.MVCService;
using MadeinHeavenBookStore.Models.Review;
using MadeinHeavenBookStore.Models.ShipandCoupon;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using static System.Net.WebRequestMethods;

namespace MadeinHeavenBookStore.Controllers.Services
{
	public class ReviewAPIService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly MadeinHeavenBookStoreContext _context;
		private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
		private IHttpContextAccessor _httpContextAccessor;
		private WishlistService _wishlistService;

		public ReviewAPIService(IHttpClientFactory httpClientFactory,
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
		public async Task<List<ReviewComment>> GetReview(int id)
		{
			var httpClient = _httpClientFactory.CreateClient();
			httpClient.BaseAddress = new Uri("https://localhost:7074/");

			return await httpClient.GetFromJsonAsync<List<ReviewComment>>($"api/ReviewAPI?id={id}");
		}

		public async Task saveComment(int star, string comment, int id)
		{

			var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var user = _userManager.FindByIdAsync(userId).Result;
			string email = user.Email;
		
			var httpClient = _httpClientFactory.CreateClient();
			httpClient.BaseAddress = new Uri("https://localhost:7074/");
			var response = await httpClient.PostAsync($"api/ReviewAPI/SaveComment/{star}/{comment}/{id}/{email}", new StringContent("", Encoding.UTF8, "application/json"));
		}

	}
}
