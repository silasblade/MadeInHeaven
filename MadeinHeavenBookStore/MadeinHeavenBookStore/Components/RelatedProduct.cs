using MadeinHeavenBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MadeinHeavenBookStore.Components
{
    public class RelatedProduct : ViewComponent
    {
        private readonly MadeinHeavenBookStoreContext _context;
        public RelatedProduct(MadeinHeavenBookStoreContext context)
        {
            _context = context;
        }
		public IViewComponentResult Invoke(List<Category> categories)
		{
			var relatedProducts = _context.Products
				.Include(p => p.Categories)
				.AsEnumerable()
				.Where(product => categories.Any(category => product.Categories.Contains(category)))
				.ToList();

			return View(relatedProducts);
		}
	}
}
