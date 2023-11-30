using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models.OrderDetail;
using MadeinHeavenBookStore.Models.ShipandCoupon;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MadeinHeavenBookStore.Models.MVCService
{
	public class ShopCartService
	{



		private readonly MadeinHeavenBookStoreContext _context;
		private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
		private IHttpContextAccessor _httpContextAccessor;

		public ShopCartService(MadeinHeavenBookStoreContext context, UserManager<MadeinHeavenBookStoreUser> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<List<ShopCart>> ShoppingCart()
		{
			string userID = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			List<ShopCart> shopCartsList = _context.ShopCarts
													.Include(sc => sc.Product)
													.Where(c => c.Id == userID)
													.ToList();

		
			return shopCartsList;
		}

		public async Task<Coupon> GetCouponApply()
		{
			string userID = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			ApplyCoupon Acoupon = _context.ApplyCoupons.FirstOrDefault(c => c.IdUser == userID);
			if (Acoupon == null)
			{

				Coupon couponCreate = _context.Coupons.Find(1);
				if (couponCreate == null)
				{
					couponCreate = new Coupon();
					couponCreate.remainnumber = 9999999;
					_context.Coupons.Add(couponCreate);
					_context.SaveChanges();
				}
				else
				{
					couponCreate.remainnumber = 9999999;
					_context.SaveChanges();
				}

				Acoupon = new ApplyCoupon();
				Acoupon.IdCoupon = 1;
				Acoupon.IdUser = userID;
				_context.ApplyCoupons.Add(Acoupon);
				_context.SaveChanges();
			}

			int idapply = Acoupon.IdCoupon;
			Coupon coupon = _context.Coupons.FirstOrDefault(c => c.Id == idapply);
			if (coupon == null || coupon.remainnumber == 0)
			{
				Acoupon.IdCoupon = 1;
				coupon = _context.Coupons.Find(1);
				_context.SaveChanges(true);
			}

			return coupon;
		}

		public async Task<ShipMethod> GetShipMethodApply()
		{
			string userID = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			ApplyShip Aship = _context.ApplyShips.FirstOrDefault(c => c.IdUser == userID);
			if (Aship == null)
			{
				Aship = new ApplyShip();
				Aship.IdShip = 1;
				Aship.IdUser = userID;
				_context.ApplyShips.Add(Aship);
				_context.SaveChanges();
			}
			ShipMethod shipMethod = _context.ShipMethods.FirstOrDefault(c => c.Id == Aship.IdShip);
			return shipMethod;
		}	






		public async Task MinusQuantity(int id)
		{
			string userID = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var shoppingCartCheck = _context.ShopCarts
				.SingleOrDefault(c => c.Id == userID && c.IdProduct == id);
			shoppingCartCheck.Quantity--;
			if (shoppingCartCheck.Quantity == 0)
			{
				_context.ShopCarts.Remove(shoppingCartCheck);
			}
			_context.SaveChanges();
		}

		public async Task PlusQuantity(int id)
		{

			string userID = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var shoppingCartCheck = _context.ShopCarts
				.SingleOrDefault(c => c.Id == userID && c.IdProduct == id);
			shoppingCartCheck.Quantity++;
			_context.SaveChanges();
		}

		public async Task ApplyCoupon(string Code)
		{
			string userID = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			ApplyCoupon applyCoupon = _context.ApplyCoupons
				.FirstOrDefault(c => c.IdUser == userID);
			Coupon coupon = _context.Coupons
				.FirstOrDefault(c => c.Code == Code);

			if (coupon != null)
			{
				applyCoupon.IdCoupon = coupon.Id;
				_context.SaveChanges();
			}
		}

		public async Task ApplyShip(int id)
		{
			string userID = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			ApplyShip applyShip = _context.ApplyShips
				.FirstOrDefault(c => c.IdUser == userID);
			applyShip.IdShip = id;
			_context.SaveChanges();


		}

		public async Task DeleteinCart(int id)
		{
			string userID = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			var shoppingCartCheck = _context.ShopCarts
				.SingleOrDefault(c => c.Id == userID && c.IdProduct == id);
			_context.ShopCarts.Remove(shoppingCartCheck);
			_context.SaveChanges();
		}

		public async Task<int> saveOrder(string receiver, string street, string ward, string district, string city, string phoneNumber)
		{
			string userID = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			Order order = new Order();
			order.IdUser = userID;
			order.UserClaim = receiver;
			order.street = street;
			order.telephonenumber = phoneNumber;
			order.city = city;
			order.wards = ward;
			order.district = district;
			_context.Orders.Add(order);
			_context.SaveChanges();
			List<ShopCart> shopCarts = _context.ShopCarts
				.Where(c => c.Id == userID)
				.ToList();

			if(shopCarts.Count == 0) 
			{
				return -4;
			}

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
				.FirstOrDefault(c => c.IdUser == userID);
			ShipMethod shipMethod = _context.ShipMethods
				.FirstOrDefault(c => c.Id == applyShip.IdShip);
			order.shipmethodprice = shipMethod.price;

			ApplyCoupon applyCoupon = _context.ApplyCoupons
				.FirstOrDefault(c => c.IdUser == userID);
			Coupon coupon = _context.Coupons
				.FirstOrDefault(c => c.Id == applyCoupon.IdCoupon);
			coupon.remainnumber--;
			order.discount = coupon.discount;

			order.DateTime = DateTime.Now;
			int discount = coupon.discount;
			if(coupon.percent == true)
			{
				discount = coupon.discount * total / 100;
			}	
			total = total - discount + shipMethod.price;

			order.TotalCost = total;
			order.status = "Waiting";

			_context.SaveChanges();
			return order.Id;
		}

	}
}
