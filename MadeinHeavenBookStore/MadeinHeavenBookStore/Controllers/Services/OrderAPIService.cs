using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models;
using MadeinHeavenBookStore.Controllers.Services;
using MadeinHeavenBookStore.Models.OrderDetail;
using MadeinHeavenBookStore.Models.MVCService;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;

namespace MadeinHeavenBookStore.Controllers.Services
{
    public class OrderAPIService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MadeinHeavenBookStoreContext _context;
        private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
        private IHttpContextAccessor _httpContextAccessor;
        private WishlistService _wishlistService;
        private readonly ContactAPIService _contactAPIService;


        public OrderAPIService(IHttpClientFactory httpClientFactory,
            MadeinHeavenBookStoreContext context,
            UserManager<MadeinHeavenBookStoreUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            WishlistService wishlistService,
            ContactAPIService contactAPIService)
        {
            _httpClientFactory = httpClientFactory;
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _wishlistService = wishlistService;
            _contactAPIService = contactAPIService;
        }

        public async Task <List<Order>> ListOrder(string? status)
        {
            var httpClient = _httpClientFactory.CreateClient();
			httpClient.BaseAddress = new Uri("https://localhost:7074/");
			return await httpClient.GetFromJsonAsync<List<Order>>($"api/OrderAPI/AllOrder?status={status}");
        }

        public async Task<List<Order>> ListOrderofMe(string status)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var httpClient = _httpClientFactory.CreateClient();
			httpClient.BaseAddress = new Uri("https://localhost:7074/");
			return await httpClient.GetFromJsonAsync<List<Order>>($"api/OrderAPI/MyOrders?status={status}&userId={userId}");
        }

        public async Task Delivery(int id)
        {
            Order ord = _context.Orders.Find(id);

            var user = await _userManager.FindByIdAsync(ord.IdUser);
            await _contactAPIService.ReturnEmail(user.Email, "Delivery Order",
            "Your order is being delivered.", id);
            //await _contactAPIService.ReturnEmail(user.Email, "Delivery Order",
            //   "Vai chuong");
            var httpClient = _httpClientFactory.CreateClient();
			httpClient.BaseAddress = new Uri("https://localhost:7074/");
			await httpClient.PostAsync($"api/OrderAPI/Delivery/{id}", new StringContent("", Encoding.UTF8, "application/json"));
        }

        public async Task DoneOrder(int id)
        {
            Order ord = _context.Orders.Find(id);
            var user = await _userManager.FindByIdAsync(ord.IdUser);
            await _contactAPIService.ReturnEmail(user.Email, "Done Order",
            "Your order has been delivered successfully.", id);
            var httpClient = _httpClientFactory.CreateClient();
			httpClient.BaseAddress = new Uri("https://localhost:7074/");
			await httpClient.PostAsync($"api/OrderAPI/Complete/{id}", new StringContent("", Encoding.UTF8, "application/json"));
        }

        public async Task CancleOrder(int id)
        {
            Order ord = _context.Orders.Find(id);
            var user = await _userManager.FindByIdAsync(ord.IdUser);
            await _contactAPIService.ReturnEmail(user.Email, "Cancle Order",
            "Your order has been cancled.", id);
            var httpClient = _httpClientFactory.CreateClient();
			httpClient.BaseAddress = new Uri("https://localhost:7074/");
			await httpClient.PostAsync($"api/OrderAPI/Cancle/{id}", new StringContent("", Encoding.UTF8, "application/json"));
        }
    }
}
