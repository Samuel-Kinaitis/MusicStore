using Microsoft.EntityFrameworkCore;
using MusicStore.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
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
builder.Services.AddServerSideBlazor();

// App Identity Coonection starts here
connectionString = builder.Configuration.GetConnectionString("MusicIdentityConnection").Replace("[DataDirectory]", path);

builder.Services.AddDbContext<AppMusicIdentityDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
	.AddEntityFrameworkStores<AppMusicIdentityDbContext>();

// end of connection string change


var app = builder.Build();

if (app.Environment.IsProduction())
{
	app.UseExceptionHandler("/error");
}
app.UseRequestLocalization(opts => {
	opts.AddSupportedCultures("en-US")
	.AddSupportedUICultures("en-US")
	.SetDefaultCulture("en-US");
});

app.UseStaticFiles();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

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

app.MapDefaultControllerRoute();
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");

SeedData.EnsurePopulated(app);
IdentitySeedData.EnsurePopulated(app);
app.Run();