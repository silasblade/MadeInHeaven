using MadeinHeavenBookStore.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace MadeinHeavenBookStore.Models.Review
{
	public class ReviewComment
	{
		[Key] public int Id { get; set; }
		public Product Product { get; set; }
		public string email { get; set; }
		[Required] public string Comment { get; set; }

		[Range(0, 5)]
		public int star {  get; set; } = 0;

	}
}
