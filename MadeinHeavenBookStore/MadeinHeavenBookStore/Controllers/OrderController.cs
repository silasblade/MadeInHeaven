using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models.ShipandCoupon;
using MadeinHeavenBookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MadeinHeavenBookStore.Models.OrderDetail;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MadeinHeavenBookStore.Controllers
{
	[Authorize]
	public class OrderController : Controller
	{
		private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
		private readonly MadeinHeavenBookStoreContext _context;


		public OrderController(UserManager<MadeinHeavenBookStoreUser> userManager, MadeinHeavenBookStoreContext context)
		{
			this._userManager = userManager;
			this._context = context;
		}
		public IActionResult Order()
		{
			string userID = _userManager.GetUserId(this.User);
			List<ShopCart> shopCartsList = _context.ShopCarts
													.Include(sc => sc.Product)
													.Where(c => c.Id == userID)
													.ToList();
			ApplyShip Aship = _context.ApplyShips.FirstOrDefault(c => c.IdUser == _userManager.GetUserId(this.User));
			if(Aship == null)
			{
				Aship = new ApplyShip();
				Aship.IdShip = 1;
				Aship.IdUser = _userManager.GetUserId(this.User);
				_context.ApplyShips.Add(Aship);
				_context.SaveChanges();
			}	

			ApplyCoupon Acoupon = _context.ApplyCoupons.FirstOrDefault(c => c.IdUser == _userManager.GetUserId(this.User));

			int idapply = Acoupon.IdCoupon;
			Coupon coupon = _context.Coupons.FirstOrDefault(c => c.Id == idapply);

			ShipMethod shipMethod = _context.ShipMethods.FirstOrDefault(c => c.Id == Aship.IdShip);


			ViewData["shipname"] = shipMethod.Name;
			ViewData["shipprice"] = shipMethod.price;
			ViewData["discount"] = coupon.discount;
			ViewData["code"] = coupon.Code;

			ShipMethod shipMethods1 = _context.ShipMethods.Find(1);
			ShipMethod shipMethods2 = _context.ShipMethods.Find(2);
			ViewData["ShipMethods1"] = shipMethods1;
			ViewData["ShipMethods2"] = shipMethods2;


		

			return View(shopCartsList);
		}

		public IActionResult ApplyShip(int id)
		{
			ApplyShip applyShip = _context.ApplyShips
				.FirstOrDefault(c => c.IdUser == _userManager.GetUserId(this.User));

			applyShip.IdShip = id;
			_context.SaveChanges();


			return RedirectToAction("Order");
		}

		[HttpPost]
		public IActionResult saveOrder(string receiver, string street, string ward, string district, string city, string phoneNumber)
		{
            Order order = new Order();
			order.IdUser = _userManager.GetUserId(this.User);
			order.UserClaim = receiver;
			order.street = street;
			order.telephonenumber = phoneNumber;
			order.city = city;
			order.wards = ward;
			order.district = district;
            _context.Orders.Add(order);
            _context.SaveChanges();
            List<ShopCart> shopCarts = _context.ShopCarts
				.Where(c => c.Id == _userManager
				.GetUserId(this.User))
				.ToList();

			int total = 0;

			foreach (var shp in shopCarts)
			{
				OrderProduct Oproduct = new OrderProduct();
				Oproduct.ProductId = shp.IdProduct;
				Oproduct.Product = _context.Products.Find(shp.IdProduct);
				Oproduct.Quantity = shp.Quantity;
				Oproduct.OrderId = order.Id;
				_context.OrderProducts.Add(Oproduct);
				_context.SaveChanges();

				Product product = _context.Products.Find(shp.IdProduct);
				total = total + product.Price * shp.Quantity;
				
			}

			ApplyShip applyShip = _context.ApplyShips
				.FirstOrDefault(c => c.IdUser == _userManager.GetUserId(this.User));
			ShipMethod shipMethod = _context.ShipMethods
				.FirstOrDefault(c => c.Id == applyShip.IdShip);
			order.shipmethodprice = shipMethod.price;

			ApplyCoupon applyCoupon = _context.ApplyCoupons
				.FirstOrDefault(c => c.IdUser == _userManager.GetUserId(this.User));
			Coupon coupon = _context.Coupons
				.FirstOrDefault(c => c.Id == applyCoupon.IdCoupon);
			order.discount = coupon.discount;

			order.DateTime = DateTime.Now;
			total = total - coupon.discount + shipMethod.price;

			order.TotalCost = total;
			order.status = "Delivering..";

			_context.SaveChanges();

			return RedirectToAction("OrderDetail", new {id = order.Id} );
		}



		public IActionResult OrderList()
		{
			List<Order> orders = _context.Orders
				.Where(c => c.IdUser == _userManager.GetUserId(this.User)).ToList();

			return View(orders);
		}

		[HttpGet]
        public IActionResult OrderDetail(int id)
        {
			Order order = _context.Orders.Find(id);
			List<OrderProduct> orderProductsList = _context.OrderProducts
				
				.Where(c=> c.OrderId == id)
				.Include(pr => pr.Product)
                .ToList();


            List<int> productIds = orderProductsList.Select(op => op.ProductId).ToList();
            List<Product> productsList = _context.Products.Where(p => productIds.Contains(p.IdProduct)).ToList(); 
            ViewData["ProductList"] = orderProductsList;


            return View(order);
        }





    }
}
