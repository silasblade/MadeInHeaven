using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Controllers
{
	[Authorize]
	public class PagesController : Controller
	{
		public IActionResult Shop()
		{
			ViewData["Title"] = "Shop";
			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Title"] = "Contact";
			return View();
		}

		public IActionResult ShoppingCart()
		{
			ViewData["Title"] = "Shopping Cart";
			return View();
		}

		public IActionResult About()
		{
			ViewData["Title"] = "About";
			return View();
		}

		public IActionResult ProductDetail()
		{
			ViewData["Title"] = "Product Detail";
			return View();
		}
	}
}
