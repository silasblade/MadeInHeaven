using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MadeinHeavenBookStore.Models;
using MadeinHeavenBookStore.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using MadeinHeavenBookStore.Models.MVCService;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Azure;
using Microsoft.AspNetCore.Authorization;

namespace MadeinHeavenBookStore.Controllers.ShopAPI
{
	[Route("api/WishlistAPI")]
	[ApiController]
	public class WishlistAPI : ControllerBase
	{
		private readonly MadeinHeavenBookStoreContext _context;
		private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
		private IHttpContextAccessor _httpContextAccessor;
		private WishlistService _wishlistService;

		public WishlistAPI(MadeinHeavenBookStoreContext context, UserManager<MadeinHeavenBookStoreUser> userManager, IHttpContextAccessor httpContextAccessor, WishlistService wishlistService)
		{
			_context = context;
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
			_wishlistService = wishlistService;
		}

		[HttpGet]
		public async Task<ActionResult<List<Wishlist>>> GetWishlist(string idUser)
		{


			Console.WriteLine(idUser + "Call an ambulance");
			//List<Wishlist> wishlists = await _wishlistService.Wishlists();
			List<Wishlist> wishlists = await _context.Wishlists
				.Include(c => c.Product)
				.Where(c => c.IdUser == idUser)
				.ToListAsync();
			return Ok(wishlists); // Trả về JSON với StatusCode 200
		}

		[HttpDelete]
		[Route("RemoveFromWishlist/{id}")]
		public async Task<IActionResult> RemoveFromWishlist(int id)
		{
			Wishlist wishlist = await _context.Wishlists.FindAsync(id);
			if (wishlist == null)
			{
				return NotFound(); // Trả về 404 nếu không tìm thấy wishlist
			}
			_context.Wishlists.Remove(wishlist);
			await _context.SaveChangesAsync();
			return NoContent(); // Trả về 204 (No Content) nếu xóa thành công
		}


	}
}
