using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models.OrderDetail;
using MadeinHeavenBookStore.Models.ShipandCoupon;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MadeinHeavenBookStore.Models;

public class MadeinHeavenBookStoreContext : IdentityDbContext<MadeinHeavenBookStoreUser>
{
    public MadeinHeavenBookStoreContext(DbContextOptions<MadeinHeavenBookStoreContext> options)
        : base(options)
    {
    }

    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ApplyShip> ApplyShips {  get; set; }
    public DbSet<ShipMethod> ShipMethods { get; set; }
    public DbSet<Product> Products { get; set; }
	public DbSet<ShopCart> ShopCarts { get; set; }
	public DbSet<Category> Categories { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
	public DbSet<ApplyCoupon> ApplyCoupons { get; set; }
	protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
 
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
