using MadeinHeavenBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Controllers
{

    [Authorize]

    public class ProductDetailController : Controller
    {
		private readonly MadeinHeavenBookStoreContext _context;

		public ProductDetailController(MadeinHeavenBookStoreContext context)
		{
			_context = context;
		}
		public IActionResult ProductDetail(int id)
        {
			var product = _context.Products.Find(id);
			return View(product);
        }
    }
}
