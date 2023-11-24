using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

namespace MadeinHeavenBookStore.Controllers
{
    public class WishlistController : Controller
    {
		private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
		private readonly MadeinHeavenBookStoreContext _context;


		public WishlistController(UserManager<MadeinHeavenBookStoreUser> userManager, MadeinHeavenBookStoreContext context)
		{
			this._userManager = userManager;
			this._context = context;
		}

		public IActionResult AddtoWishlist(int id)
		{
			Product product = _context.Products.Find(id);
			Wishlist wishlistcheck = _context.Wishlists
				.FirstOrDefault(w => w.Product == product && w.IdUser == _userManager.GetUserId(this.User));
			if(wishlistcheck != null)
			{
				return RedirectToAction("Wishlist");
			}	
			Wishlist wishlist = new Wishlist();
			wishlist.Product = product;
			wishlist.IdUser = _userManager.GetUserId(this.User);
			_context.Wishlists.Add(wishlist);
			_context.SaveChanges();
			return RedirectToAction("Wishlist");
		}

		[HttpPost]
		public IActionResult RemovefromWishlist(int id)
		{
			Wishlist wishlist = _context.Wishlists.Find(id);
			_context.Wishlists.Remove(wishlist);
			_context.SaveChanges();
			return RedirectToAction("Wishlist");
		}
		public IActionResult Wishlist()
		{
			List<Wishlist> wishlists = _context.Wishlists
				.Include(c=> c.Product)
				.Where(c => c.IdUser == _userManager
				.GetUserId(this.User)).ToList();
			return View(wishlists);
		}
	}
}
