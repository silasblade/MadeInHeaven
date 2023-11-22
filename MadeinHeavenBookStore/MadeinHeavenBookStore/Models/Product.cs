using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace MadeinHeavenBookStore.Models
{
	public class Product
	{
		[Required]
		[BindNever]
		public int IdProduct { get; set; }
		public string NameProduct { get; set; } = string.Empty;
		public string Author { get; set; } = string.Empty;
		public string Publishing { get; set; } = string.Empty;
		public string Set { get; set; } = string.Empty;
	}
}
