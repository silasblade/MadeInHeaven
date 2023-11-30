using MadeinHeavenBookStore.Models.ShipandCoupon;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

            if (!context.Coupons.Any())
            {
				context.Coupons.AddRange(
					new ShipandCoupon.Coupon
					{
						Code = "coupon",
						discount = 0,
						Name = "coupon",
						percent = false,
						remainnumber = 9999999,
						Description = "coupon"
					},

					new ShipandCoupon.Coupon
                    {
  
                        Code = "ab",
                        discount = 12997,
                        Name = "coupon",
                        percent = false,
                        remainnumber = 9999999,
                        Description = "coupon"
                    },


                    new ShipandCoupon.Coupon
                    {

                        Code = "ab2",
                        discount = 17997,
                        Name = "coupon",
                        percent = false,
                        remainnumber = 9999999,
                        Description = "coupon"
                    }

                    ) ;
                context.SaveChanges();

            }

            if (!context.ShipMethods.Any())
			{
				context.ShipMethods.AddRange(
					new ShipandCoupon.ShipMethod
					{
						Name = "Quick Ship",
						Description = "About 6 hours, and About 3 day if you are not in Ho Chi Minh City",
						price = 39000
					},

					new ShipandCoupon.ShipMethod
					{
						Name = "Standard Ship",
						Description = "About 2 days, and About 8 day if you are not in Ho Chi Minh City",
						price = 22000
					}

					) ;
                context.SaveChanges();

            }

            if (!context.Categories.Any())
			{
				context.Categories.AddRange(
					new Category
					{
						Name = "Fantasy"
					},
					new Category
					{
						Name = "Adventure"
					},
					new Category
					{
						Name = "Mystery"
					},
					new Category
					{
						Name = "Dark"
					},
                    new Category
                    {
                        Name = "Romance"
                    },
                    new Category
                    {
                        Name = "School"
                    },
                    new Category
                    {
                        Name = "Slice of Life"
                    },
                    new Category
                    {
                        Name = "Tragedy"
                    },
                    new Category
                    {
                        Name = "Chill"
                    },
                    new Category
                    {
                        Name = "Learning"
                    },
                    new Category
                    {
                        Name = "Toys"
                    },
                    new Category
                    {
                        Name = "Accessory"
                    }
                    );
                context.SaveChanges();
            }
			if (!context.Products.Any())
			{
				context.Products.AddRange(
					new Product
					{
						NameProduct = "Harry potter",
						Price = 100000,
						Categories = context.Categories.Where(c => c.Name == "Fantasy" || c.Name == "Adventure").ToList(),
						Description = "Chúa tể bóng tối đã trỗi dậy, hãy đánh bại ả.",
						imageurl1 = "https://img-cdn.2game.vn/pictures/xemgame/2018/11/22/e83fbb79-zoe-1.jpg",
						imageurl2 = "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/01/zoe-2.jpg",
						imageurl3 = "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/01/zoe-2.jpg"

					},
					 new Product
					 {
						 NameProduct = "Sách lậu",
						 Price = 100000,
						 Categories = context.Categories.Where(c => c.Name == "Fantasy" || c.Name == "Adventure").ToList(),
						 Description = "Chúa tể bóng tối đã trỗi dậy, hãy đánh bại ả.",
						 imageurl1 = "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/01/zoe-2.jpg",
						 imageurl2 = "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/01/zoe-2.jpg",
						 imageurl3 = "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/01/zoe-2.jpg"
					 },

					new Product
					{
						NameProduct = "Sách lậu",
						Price = 100000,
						Categories = context.Categories.ToList(),
						Description = "Chúa tể bóng tối đã trỗi dậy, hãy đánh bại ả.",
						imageurl1 = "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/01/zoe-2.jpg",
						imageurl2 = "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/01/zoe-2.jpg",
						imageurl3 = "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/01/zoe-2.jpg"
					},
					new Product
					{
						NameProduct = "Tôi thấy hoa vàng trên cỏ xanh",
						Price = 100000,
						Categories = context.Categories.ToList(),
						Description = "Chúa tể bóng tối đã trỗi dậy, hãy đánh bại ả.",
						imageurl1 = "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/01/zoe-2.jpg",
						imageurl2 = "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/01/zoe-2.jpg",
						imageurl3 = "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/01/zoe-2.jpg"

					},
					new Product
					{
						NameProduct = "Cánh diều tuổi thơ",
						Price = 100000,
						Categories = context.Categories.ToList(),
						Description = "Chúa tể bóng tối đã trỗi dậy, hãy đánh bại ả.",
						imageurl1 = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Vex_0.jpg",
						imageurl2 = "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/01/zoe-2.jpg",
						imageurl3 = "https://cdn.sforum.vn/sforum/wp-content/uploads/2023/01/zoe-2.jpg"

					},
					new Product
					{
						NameProduct = "Harry Bình",
						Price = 100000,
						Categories = context.Categories.ToList(),
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
