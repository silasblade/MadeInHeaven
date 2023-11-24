using MadeinHeavenBookStore.Models;
using MadeinHeavenBookStore.Models.Review;

namespace MadeinHeavenBookStore.Controllers
{
	internal class ProductDetailViewModel
	{
		public List<ReviewComment> ReviewComment { get; set; }
		public Product Product { get; set; }
		public List<Category> Categories { get; set; }
	}
}