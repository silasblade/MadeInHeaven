using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace MadeinHeavenBookStore.Models
{
	public class Product
	{
		[Required]
		[BindNever]
		[Key]
		public int IdProduct { get; set; }
		public string NameProduct { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Author { get; set; } = string.Empty;
		public string Publishing { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string Category { get; set; } = string.Empty;
        public string Set { get; set; } = string.Empty;
	}
}
