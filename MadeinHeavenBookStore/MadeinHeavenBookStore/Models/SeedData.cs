using Microsoft.EntityFrameworkCore;

namespace MadeinHeavenBookStore.Models
{
	public static class SeedData
	{
		public static void EnsurePopulated(IApplicationBuilder app)
		{
			MadeinHeavenBookStoreContext context = app.ApplicationServices
				.CreateScope()
				.ServiceProvider
				.GetRequiredService<MadeinHeavenBookStoreContext>();
			if (context.Database.GetPendingMigrations().Any())
			{
				context.Database.Migrate();
			}
			if (!context.Products.Any())
			{
				context.Products.AddRange(
					new Product
					{
						NameProduct = "Sách lậu",
						Price = 100000,
						Category = "DarkBook",
						Description = "Chúa tể bóng tối đã trỗi dậy, hãy đánh bại ả."

					},
					new Product
					{
						NameProduct = "Tôi thấy hoa vàng trên cỏ xanh",
						Price = 100000,
						Category = "LightBook",
						Description = "Chúa tể bóng tối đã trỗi dậy, hãy đánh bại ả.",
						imageurl1 = "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/01/zoe-2.jpg",
						imageurl2 = "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/01/zoe-2.jpg",
						imageurl3 = "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/01/zoe-2.jpg"

					},
					new Product
					{
						NameProduct = "Cánh diều tuổi thơ",
						Price = 3124,
						Category = "Sách nào đó",
						Description = "Chúa tể bóng tối đã trỗi dậy, hãy đánh bại ả.",
						imageurl1 = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Vex_0.jpg",
						imageurl2 = "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/01/zoe-2.jpg",
						imageurl3 = "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/01/zoe-2.jpg"

					},
					new Product
					{
						NameProduct = "Harry Bình",
						Price = 100000,
						Category = "Fantasy",
						Description = "Chúa tể bóng tối đã trỗi dậy, hãy đánh bại ả.",
						imageurl1 = "https://img-cdn.2game.vn/pictures/xemgame/2018/11/22/e83fbb79-zoe-1.jpg",
						imageurl2 = "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/01/zoe-2.jpg",
						imageurl3 = "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/01/zoe-2.jpg"

					}
				);
				context.SaveChanges();
			}
		}
	}
}
