using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models;
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
            return View(shopCartsList);
        }

		[HttpPost]
		public IActionResult MinusQuantity(int id)
		{

			var shoppingCartCheck = _context.ShopCarts
				.SingleOrDefault(c => c.Id == _userManager.GetUserId(this.User) && c.IdProduct == id);
			shoppingCartCheck.Quantity--;
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

		

	


	}
}
