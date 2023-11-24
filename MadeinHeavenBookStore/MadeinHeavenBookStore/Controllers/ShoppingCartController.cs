using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models;
using MadeinHeavenBookStore.Models.ShipandCoupon;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MadeinHeavenBookStore.Controllers
{
    [Authorize]

    public class ShoppingCartController : Controller
    {
		private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
		private readonly MadeinHeavenBookStoreContext _context;


		public ShoppingCartController(UserManager<MadeinHeavenBookStoreUser> userManager, MadeinHeavenBookStoreContext context)
		{
			this._userManager = userManager;
			this._context = context;
		}
		public IActionResult ShoppingCart()
        {
            string userID = _userManager.GetUserId(this.User);
            List<ShopCart> shopCartsList = _context.ShopCarts
													.Include(sc => sc.Product)
													.Where(c => c.Id == userID)
                                                    .ToList();

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

			ApplyCoupon Acoupon = _context.ApplyCoupons.FirstOrDefault(c => c.IdUser == _userManager.GetUserId(this.User));
			if (Acoupon == null)
			{
				Acoupon = new ApplyCoupon();
				Acoupon.IdCoupon = 1;
				Acoupon.IdUser = _userManager.GetUserId(this.User);
				_context.ApplyCoupons.Add(Acoupon);
				_context.SaveChanges();
			}

			int idapply = Acoupon.IdCoupon;
			Coupon coupon = _context.Coupons.FirstOrDefault(c => c.Id == idapply);
			if(coupon == null)
			{
				Acoupon.IdCoupon = 1;
				coupon = _context.Coupons.Find(1);
				_context.SaveChanges(true);
			}

			ViewData["discount"] = coupon.discount;
			ViewData["code"] = coupon.Code;

			return View(shopCartsList);
        }

		[HttpPost]
		public IActionResult MinusQuantity(int id)
		{

			var shoppingCartCheck = _context.ShopCarts
				.SingleOrDefault(c => c.Id == _userManager.GetUserId(this.User) && c.IdProduct == id);
			shoppingCartCheck.Quantity--;
			if(shoppingCartCheck.Quantity == 0)
			{
				_context.ShopCarts.Remove(shoppingCartCheck);
			}	
			_context.SaveChanges();
			return RedirectToAction("ShoppingCart");
		}

		[HttpPost]
		public IActionResult PlusQuantity(int id) 
		{


			var shoppingCartCheck = _context.ShopCarts
				.SingleOrDefault(c => c.Id == _userManager.GetUserId(this.User) && c.IdProduct == id);
			shoppingCartCheck.Quantity++;
			_context.SaveChanges();
			return RedirectToAction("ShoppingCart");
		}

		[HttpPost]
		public IActionResult ApplyCoupon(string Code)
		{
			

			ApplyCoupon applyCoupon = _context.ApplyCoupons
				.FirstOrDefault(c => c.IdUser == _userManager.GetUserId(this.User));
		

			Coupon coupon = _context.Coupons
				.FirstOrDefault(c => c.Code == Code);

			if(coupon == null)
			{
				return RedirectToAction("ShoppingCart");
			}	
			else
			{
				applyCoupon.IdCoupon = coupon.Id;
				_context.SaveChanges();
			}
			
			return RedirectToAction("ShoppingCart");
		}

		[HttpPost]
		public IActionResult DeleteinCart(int id)
		{
			var shoppingCartCheck = _context.ShopCarts
				.SingleOrDefault(c => c.Id == _userManager.GetUserId(this.User) && c.IdProduct == id);
				_context.ShopCarts.Remove(shoppingCartCheck);
				_context.SaveChanges();
			return RedirectToAction("ShoppingCart");
		}






	}
}
