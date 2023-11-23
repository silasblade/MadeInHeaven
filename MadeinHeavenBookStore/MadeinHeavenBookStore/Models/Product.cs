using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MadeinHeavenBookStore.Models
{
	public class Product
	{
		[Required]
		[BindNever]
		[Key]
		public int IdProduct { get; set; }
		public string NameProduct { get; set; } = string.Empty;

		[Required]
		public int Price { get; set; } = 1000777777;
        public string Author { get; set; } = "Chúa tể bóng tối";
		public string Publishing { get; set; } = "NXB Màn đêm";
		public string Description { get; set; } = string.Empty;
		[Required(ErrorMessage = "Please enter a category")]
		public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
		public string imageurl1 { get; set; } = "https://files.cults3d.com/uploaders/23262976/illustration-file/77dac2dc-08a2-4e27-be3c-5ffc190f8a91/ref-2.png";
		public string imageurl2 { get; set; } = "https://files.cults3d.com/uploaders/23262976/illustration-file/77dac2dc-08a2-4e27-be3c-5ffc190f8a91/ref-2.png";
		public string imageurl3 { get; set; } = "https://files.cults3d.com/uploaders/23262976/illustration-file/77dac2dc-08a2-4e27-be3c-5ffc190f8a91/ref-2.png";
		public string Set { get; set; } = string.Empty;
	}
}
