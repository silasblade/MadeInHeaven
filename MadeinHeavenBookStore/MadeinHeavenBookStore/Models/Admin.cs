using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace MadeinHeavenBookStore.Models
{
	public class Admin
	{
		[Required]
		[BindNever]
		public int IdAdmin { get; set; }

		[Required]
		[BindNever]
		public string AdminName { get; set; } = string.Empty;
		public string AdminPass { get; set; } = string.Empty;
	}
}
