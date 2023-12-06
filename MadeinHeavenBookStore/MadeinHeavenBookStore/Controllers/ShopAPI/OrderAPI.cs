using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models;
using MadeinHeavenBookStore.Controllers.Services;

using MadeinHeavenBookStore.Models.MVCService;
using MadeinHeavenBookStore.Models.Review;
using MadeinHeavenBookStore.Models.OrderDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using static System.Net.WebRequestMethods;
namespace MadeinHeavenBookStore.Controllers.ShopAPI
{
    [Route("api/OrderAPI")]
    [ApiController]
    public class OrderAPI : ControllerBase
    {
        private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
        private readonly MadeinHeavenBookStoreContext _context;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly ContactAPIService _contactAPIService;


        public OrderAPI(UserManager<MadeinHeavenBookStoreUser> userManager, 
            MadeinHeavenBookStoreContext context, 
            IHttpContextAccessor httpContextAccessor,
            ContactAPIService contactAPIService)
        {
            this._userManager = userManager;
            this._context = context;
            _httpContextAccessor = httpContextAccessor;
            _contactAPIService = contactAPIService;

        }

        [HttpGet]
        [Route("AllOrder")]
        public async Task<ActionResult<List<Order>>> ListOrder(string? status)
        {
            Console.WriteLine("Order API CALL");
            List<Order> orders =  _context.Orders.ToList();
            if (!string.IsNullOrWhiteSpace(status))
            {
                orders = orders.Where(s => s.status.Contains(status)).ToList();
            }
            return Ok(orders);
        }

        [HttpGet]
        [Route("MyOrders")]
        public async Task<ActionResult<List<Order>>> ListOrderofMe(string? status, string userId)
        {
   
            List<Order> orders = _context.Orders
                .Where(c => c.IdUser == userId)
                .ToList();
            if (!string.IsNullOrWhiteSpace(status))
            {
                orders = orders.Where(s => s.status.Contains(status)).ToList();
            }
            return Ok(orders);
        }

        [HttpPost]
        [Route("Delivery/{id}")]
        public async Task<ActionResult> Delivery(int id)
        {
            Order order = _context.Orders.Find(id);
            order.status = "Delivering...";
            _context.SaveChanges();
           
            return Ok();
        }

        [HttpPost]
        [Route("Complete/{id}")]
        public async Task<ActionResult> DoneOrder(int id)
        {
            Order order = _context.Orders.Find(id);
            order.status = "Completed";
            _context.SaveChanges();
            
            return Ok();
        }

        [HttpPost]
        [Route("Cancle/{id}")]
        public async Task<ActionResult> CancleOrder(int id)
        {
            Order order = _context.Orders.Find(id);
            order.status = "Cancled";
            _context.SaveChanges();
            return Ok();
        }
    }
}
