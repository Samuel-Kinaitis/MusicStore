using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace MusicStore.Models
{
	public static class IdentitySeedData
	{
		private const string adminUser = "Admin";
		private const string adminPassword = "Secret123$";
		public static async void EnsurePopulated(IApplicationBuilder app)
		{
			AppMusicIdentityDbContext context = app.ApplicationServices
			.CreateScope().ServiceProvider
			.GetRequiredService<AppMusicIdentityDbContext>();
			if (context.Database.GetPendingMigrations().Any())
			{
				context.Database.Migrate();
			}
			UserManager<IdentityUser> userManager = app.ApplicationServices
			.CreateScope().ServiceProvider
			.GetRequiredService<UserManager<IdentityUser>>();
			IdentityUser user = await userManager.FindByNameAsync(adminUser);
			if (user == null)
			{
				user = new IdentityUser("Admin");
				user.Email = "admin@example.com";
				user.PhoneNumber = "555-1234";
				await userManager.CreateAsync(user, adminPassword);
			}
		}
	}
}