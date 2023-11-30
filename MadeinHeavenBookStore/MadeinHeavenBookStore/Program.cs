using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models;
using MadeinHeavenBookStore.Data;
using MadeinHeavenBookStore.Models.ShipandCoupon;
using Blazored.Toast;
using MadeinHeavenBookStore.Models.MVCService;
using MadeinHeavenBookStore.Controllers;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MadeinHeavenBookStoreContextConnection") ?? throw new InvalidOperationException("Connection string 'MadeinHeavenBookStoreContextConnection' not found.");

//Blazor
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddServerSideBlazor();



builder.Services.AddDbContext<MadeinHeavenBookStoreContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<MadeinHeavenBookStoreUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MadeinHeavenBookStoreContext>();

// Add services to the container.
builder.Services.AddHttpClient<CouponService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7074/");
});
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<CouponService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ShopController>();
builder.Services.AddScoped<ShopService>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<ShopCartService>();


builder.Services.AddSingleton<EmailSender>();



builder.Services.AddBlazoredToast();


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
app.UseCors(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//BLazor 2
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
});
app.MapBlazorHub();
app.UseBlazorFrameworkFiles();
app.MapFallbackToPage("/_Host");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
SeedData.EnsurePopulated(app);


app.Run();
