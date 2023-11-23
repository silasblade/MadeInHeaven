using MadeinHeavenBookStore.Models;

namespace MadeinHeavenBookStore.Controllers
{
	internal class ProductDetailViewModel
	{
		public Product Product { get; set; }
		public List<Category> Categories { get; set; }
	}
}