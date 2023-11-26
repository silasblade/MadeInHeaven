using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models;
using MadeinHeavenBookStore.Models.OrderDetail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace MadeinHeavenBookStore.Controllers.AdminManage
{
	public class OrderCheckController : Controller
	{
		private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
		private readonly MadeinHeavenBookStoreContext _context;


		public OrderCheckController(UserManager<MadeinHeavenBookStoreUser> userManager, MadeinHeavenBookStoreContext context)
		{
			this._userManager = userManager;
			this._context = context;
		}
		public IActionResult OrderCheck(string? status, int? page)
		{
            List<Order> orders = _context.Orders
                .ToList();
            if (!string.IsNullOrWhiteSpace(status))
			{
				orders = orders.Where(s => s.status.Contains(status)).ToList();
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(orders.ToPagedList(pageNumber, pageSize));
		}

		public IActionResult Delivery(int id)
		{
			Order order = _context.Orders.Find(id);
			order.status = "Delivering...";
			_context.SaveChanges();
			return RedirectToAction("OrderCheck");
		}

        public IActionResult DoneOrder(int id)
        {
            Order order = _context.Orders.Find(id);
            order.status = "Completed";
            _context.SaveChanges();
            return RedirectToAction("OrderCheck");
        }

        public IActionResult CancleOrder(int id)
        {
            Order order = _context.Orders.Find(id);
            order.status = "Cancled";
            _context.SaveChanges();
            return RedirectToAction("OrderCheck");
        }
    }
}
