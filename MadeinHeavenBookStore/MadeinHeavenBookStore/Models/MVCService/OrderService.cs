using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models.OrderDetail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MadeinHeavenBookStore.Models.MVCService
{
    public class OrderService
    {
        private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
        private readonly MadeinHeavenBookStoreContext _context;
        private IHttpContextAccessor _httpContextAccessor;


        public OrderService(UserManager<MadeinHeavenBookStoreUser> userManager, MadeinHeavenBookStoreContext context, IHttpContextAccessor httpContextAccessor)
        {
            this._userManager = userManager;
            this._context = context;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task <List<Order>> ListOrder(string status)
        {
            List<Order> orders =  _context.Orders.ToList();
            if (!string.IsNullOrWhiteSpace(status))
            {
                orders = orders.Where(s => s.status.Contains(status)).ToList();
            }

            return orders;
        }

        public async Task<List<Order>> ListOrderofMe(string status)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

   
            List<Order> orders = _context.Orders
                .Where(c => c.IdUser == userId)
                .ToList();
            if (!string.IsNullOrWhiteSpace(status))
            {
                orders = orders.Where(s => s.status.Contains(status)).ToList();
            }

            return orders;
        }

        public async Task Delivery(int id)
        {
            Order order = _context.Orders.Find(id);
            order.status = "Delivering...";
            _context.SaveChanges();
        }

        public async Task DoneOrder(int id)
        {
            Order order = _context.Orders.Find(id);
            order.status = "Completed";
            _context.SaveChanges();
        }

        public async Task CancleOrder(int id)
        {
            Order order = _context.Orders.Find(id);
            order.status = "Cancled";
            _context.SaveChanges();
        }
    }
}
