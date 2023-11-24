﻿using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MadeinHeavenBookStore.Controllers
{

    [Authorize]

    public class ProductDetailController : Controller
    {
		private readonly MadeinHeavenBookStoreContext _context;
		private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
		public ProductDetailController(MadeinHeavenBookStoreContext context, UserManager<MadeinHeavenBookStoreUser> userManager)
		{
			_context = context;
			this._userManager = userManager;
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









		[HttpPost]
		public async Task<IActionResult> AddToCart(int id)
		{
			// Xử lý logic thêm sản phẩm vào giỏ hàng với id được truyền vào
			// Ví dụ: Tìm sản phẩm theo id, thêm vào giỏ hàng, và chuyển hướng đến trang giỏ hàng
			var shoppingCartCheck = _context.ShopCarts
				.SingleOrDefault(c => c.Id == _userManager.GetUserId(this.User) && c.IdProduct == id);
			if (shoppingCartCheck == null)
			{
				var product = _context.Products.Find(id);
				var currentUser = await _userManager.GetUserAsync(this.User);


				var shopcart = new ShopCart
				{
					Product = product,
					IdProduct = id,
					Id = _userManager.GetUserId(this.User),
					MadeinHeavenBookStoreUser = currentUser
				};
				_context.ShopCarts.Add(shopcart);
				_context.SaveChanges();
			}
			else
			{
				shoppingCartCheck.Quantity++;
				_context.SaveChanges();
			}
			TempData["SuccessMessage"] = "You have successfully added the product to your cart.";



			// Chuyển hướng đến trang giỏ hàng hoặc trang sản phẩm sau khi thêm vào giỏ hàng
			return RedirectToAction("ProductDetail", new { id = id }); // Thay "ShoppingCart" bằng tên action hoặc route cụ thể
		}







	}




}
