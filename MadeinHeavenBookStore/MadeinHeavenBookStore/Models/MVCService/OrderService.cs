using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models.OrderDetail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Models.MVCService
{
    public class OrderService
    {
        private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
        private readonly MadeinHeavenBookStoreContext _context;


        public OrderService(UserManager<MadeinHeavenBookStoreUser> userManager, MadeinHeavenBookStoreContext context)
        {
            this._userManager = userManager;
            this._context = context;
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
