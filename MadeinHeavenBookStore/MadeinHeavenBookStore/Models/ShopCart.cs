using MadeinHeavenBookStore.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadeinHeavenBookStore.Models
{
	public class ShopCart
	{
		[Required]
		[Key]
		public int IdShopCart { get; set; }
		[Required]
		public MadeinHeavenBookStoreUser? MadeinHeavenBookStoreUser { get; set; } = new MadeinHeavenBookStoreUser();
		public string Id { get; set; }

		public Product Product { get; set; }
		[Required]
		public int IdProduct { get; set; }

		public int Quantity { get; set; } = 1;

		
	}
}
