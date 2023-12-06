using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MadeinHeavenBookStore.Models;
using MadeinHeavenBookStore.Models.Review;
using MadeinHeavenBookStore.Models.MVCService;

using MadeinHeavenBookStore.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using MadeinHeavenBookStore.Models.MVCService;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Azure;
using Microsoft.AspNetCore.Authorization;

namespace MadeinHeavenBookStore.Controllers.ShopAPI
{

	[Route("api/ReviewAPI")]
	[ApiController]
	public class ReviewAPI : ControllerBase
	{
		private readonly MadeinHeavenBookStoreContext _context;
		private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
		private IHttpContextAccessor _httpContextAccessor;
		private readonly ReviewService _reviewService;

		public ReviewAPI(MadeinHeavenBookStoreContext context, UserManager<MadeinHeavenBookStoreUser> userManager, IHttpContextAccessor httpContextAccessor, ReviewService reviewService)
		{
			_context = context;
			this._userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
			_reviewService = reviewService;
		}

		[HttpGet]
		public async Task<ActionResult<List<ReviewComment>>> GetReview(int id)
		{
			var product = await _context.Products
				.FirstOrDefaultAsync(c => c.IdProduct == id);

			List<ReviewComment> comments = _context.ReviewComments
				.Where(c => c.Product == product)
				.ToList();
			comments.Reverse();
			return Ok(comments);
		}


		[HttpPost]
		[Route("SaveComment/{star}/{comment}/{id}/{email}")]
		public async Task<IActionResult> saveComment(int star, string comment,  int id,  string email)
		{

			Console.WriteLine("===API====call");

			var product = _context.Products.Find(id);
			ReviewComment reviewComment = new ReviewComment();
			reviewComment.email = email;
			reviewComment.star = star;
			reviewComment.Product = product;
			reviewComment.Comment = comment;
			
			_context.ReviewComments.Add(reviewComment);
			_context.SaveChanges();
			return Ok();
		}


	}
}
