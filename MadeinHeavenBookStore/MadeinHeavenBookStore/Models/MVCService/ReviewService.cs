using MadeinHeavenBookStore.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using MadeinHeavenBookStore.Models.Review;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MadeinHeavenBookStore.Models.MVCService
{


	public class ReviewService
	{
		private readonly MadeinHeavenBookStoreContext _context;
		private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
		private IHttpContextAccessor _httpContextAccessor;

		public ReviewService(MadeinHeavenBookStoreContext context, UserManager<MadeinHeavenBookStoreUser> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			this._userManager = userManager;
			_httpContextAccessor = httpContextAccessor;

		}

		public async Task<List<ReviewComment>> GetAllReview(int id)
		{
			var product = _context.Products
				.Include(p => p.Categories)
				.FirstOrDefault(c => c.IdProduct == id);
			List<ReviewComment> comments = _context.ReviewComments
				.Where(c => c.Product == product)
				.ToList();
			return comments;
		}

		public async Task saveComment(int star, string comment, int id)
		{
			Console.WriteLine("functioncall fiest");
			Console.WriteLine(star + comment + id);
			Console.WriteLine("functioncall tưo");

			var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var user = _userManager.FindByIdAsync(userId).Result;

			var product = _context.Products.Find(id);
			ReviewComment reviewComment = new ReviewComment();
			reviewComment.email = user.Email;
			reviewComment.star = star;
			reviewComment.Product = product;
			reviewComment.Comment = comment;
			_context.ReviewComments.Add(reviewComment);
			_context.SaveChanges();
		}


	}
}
