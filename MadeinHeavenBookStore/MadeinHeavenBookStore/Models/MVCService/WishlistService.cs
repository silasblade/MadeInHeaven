using MadeinHeavenBookStore.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MadeinHeavenBookStore.Models.MVCService
{
	public class WishlistService
	{
		private readonly MadeinHeavenBookStoreContext _context;
		private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
		private IHttpContextAccessor _httpContextAccessor;

		public WishlistService(MadeinHeavenBookStoreContext context, UserManager<MadeinHeavenBookStoreUser> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task RemovefromWishlist(int id)
		{
			Wishlist wishlist = _context.Wishlists.Find(id);
			_context.Wishlists.Remove(wishlist);
			_context.SaveChanges();
		}
		public async Task<List<Wishlist>> Wishlists()
		{

			var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			Console.WriteLine(userId + "call SV");
			List<Wishlist> wishlists = _context.Wishlists
				.Include(c => c.Product)
				.Where(c => c.IdUser == userId).ToList();
			return wishlists;
		}
	}
}
