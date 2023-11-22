using MadeinHeavenBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Controllers
{
    [Authorize]

    public class ShopController : Controller
    {
        private readonly MadeinHeavenBookStoreContext _context;

        public ShopController(MadeinHeavenBookStoreContext context)
        {
            _context = context;
        }

        public IActionResult Shop()
        {
            var products = _context.Products.ToList();
            return View(products);
        }


		//Chọn sản phẩm rồi xem thông tin
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ShowDetail(Product product)
		{
			if (ModelState.IsValid)
			{
				// Chuyển hướng đến action ProductDetail của ProductDetailController và truyền ID
				return RedirectToAction("ProductDetail", "ProductDetail", new { id = product.IdProduct });
			}

			// Nếu ModelState không hợp lệ, hiển thị lại form với thông báo lỗi
			return View();
		}
	}
}
