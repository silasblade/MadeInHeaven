using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Controllers
{
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

		public IActionResult Product()
		{
			ViewData["Title"] = "Product Detail";
			return View();
		}
	}
}
