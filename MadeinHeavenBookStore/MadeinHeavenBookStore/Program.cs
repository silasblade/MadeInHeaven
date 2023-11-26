using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MadeinHeavenBookStoreContextConnection") ?? throw new InvalidOperationException("Connection string 'MadeinHeavenBookStoreContextConnection' not found.");

builder.Services.AddDbContext<MadeinHeavenBookStoreContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<MadeinHeavenBookStoreUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MadeinHeavenBookStoreContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
SeedData.EnsurePopulated(app);


app.Run();
