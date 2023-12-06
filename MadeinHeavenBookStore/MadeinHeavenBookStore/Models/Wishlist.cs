using MadeinHeavenBookStore.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using MadeinHeavenBookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MadeinHeavenBookStore.Models
{
	public class Wishlist
	{
		[Key]
		public int Id { get; set; }
		public MadeinHeavenBookStoreUser? User { get; set; } = new MadeinHeavenBookStoreUser();
		public string IdUser { get; set; }
		public Product Product { get; set; }
	}



}
