using Microsoft.EntityFrameworkCore;
using MusicStore.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
//
//builder.Services.AddDbContext<StoreDbContext>(opts => {
//    opts.UseSqlServer(
//});
// start connection string change
string path = Directory.GetCurrentDirectory();
var connectionString = builder.Configuration.GetConnectionString("MusicStoreConnection").Replace("[DataDirectory]", path);

builder.Services.AddDbContext<StoreDbContext>(options =>
	options.UseSqlServer(connectionString));
// end of connection string change

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();
app.UseStaticFiles();
app.UseSession();

app.MapControllerRoute("catpage",
"{category}/Page{productPage:int}",
new { Controller = "Home", action = "Index" });
app.MapControllerRoute("page", "Page{productPage:int}",
new { Controller = "Home", action = "Index", productPage = 1 });
app.MapControllerRoute("category", "{category}",
new { Controller = "Home", action = "Index", productPage = 1 });
app.MapControllerRoute("pagination",
"Products/Page{productPage}",
new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute("pagination",
"Products/Page{productPage}",
new { Controller = "Home", action = "Index" });
app.MapDefaultControllerRoute();

app.MapRazorPages();

SeedData.EnsurePopulated(app);
app.Run();