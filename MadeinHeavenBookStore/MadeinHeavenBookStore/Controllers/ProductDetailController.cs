using MadeinHeavenBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
			var product = _context.Products
		.Include(p => p.Categories)
		.FirstOrDefault(c => c.IdProduct == id);


			var viewModel = new ProductDetailViewModel
			{
				Product = product,
				Categories = product.Categories.ToList()
			};

			return View(viewModel);
		}
    }
}
