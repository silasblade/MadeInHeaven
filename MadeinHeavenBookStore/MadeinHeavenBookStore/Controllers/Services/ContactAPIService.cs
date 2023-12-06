using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models.MVCService;
using MadeinHeavenBookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace MadeinHeavenBookStore.Controllers.Services
{
	public class ContactAPIService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly MadeinHeavenBookStoreContext _context;
		private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
		private IHttpContextAccessor _httpContextAccessor;
		private WishlistService _wishlistService;

		public ContactAPIService(IHttpClientFactory httpClientFactory,
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

		public async Task SendEmail(string mail, string content)
		{
			var httpClient = _httpClientFactory.CreateClient();
			httpClient.BaseAddress = new Uri("https://localhost:7074/");
			var response = await httpClient.PostAsync($"api/ContactAPI/SendEmail/{mail}/{content}", new StringContent("", Encoding.UTF8, "application/json"));
		}

        public async Task ReturnEmail(string mail, string subject, string content, int ids)
        {
			string id = ids.ToString();
            var httpClient = _httpClientFactory.CreateClient();
			httpClient.BaseAddress = new Uri("https://localhost:7074/");
			var response = await httpClient.PostAsync($"api/ContactAPI/ReturnEmail/{mail}/{subject}/{content}/{id}", new StringContent("", Encoding.UTF8, "application/json"));
        }
    }
}
