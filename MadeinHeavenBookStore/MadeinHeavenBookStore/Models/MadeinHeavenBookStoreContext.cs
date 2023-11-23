using MadeinHeavenBookStore.Areas.Identity.Data;
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

    public DbSet<Product> Products { get; set; }
	public DbSet<ShopCart> ShopCarts { get; set; }
	public DbSet<Category> Categories { get; set; }
	protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
 
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
