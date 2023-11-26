using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MadeinHeavenBookStore.Models
{
	public class Product
	{
		[Required]
		[Key]
		public int IdProduct { get; set; }

        [Required(ErrorMessage = "Please enter the product Name.")]
        public string NameProduct { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please enter the product Price.")]
		public int Price { get; set; } = 1000777777;
        [Required(ErrorMessage = "Please enter the valid value.")]
        public string Author { get; set; } = "Chúa tể bóng tối";
        [Required(ErrorMessage = "Please enter the valid value.")]
        public string Publishing { get; set; } = "NXB Màn đêm";
        [Required(ErrorMessage = "Please enter the valid value.")]
        public string Description { get; set; } = "Cuốn sách mới";
		[Required(ErrorMessage = "Please enter a category")]
		public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
		public string imageurl1 { get; set; } = "/uploads/pompom1.png";
		public string imageurl2 { get; set; } = "/uploads/pompom2.gif";
        public string imageurl3 { get; set; } = "/uploads/pompom3.gif";
        public int Inventory {  get; set; } = 200;
		public string Set { get; set; } = string.Empty;
	}
}
