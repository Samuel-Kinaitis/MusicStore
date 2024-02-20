using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace MusicStore.Models
{
	public class AppMusicIdentityDbContext : IdentityDbContext<IdentityUser>
	{
		public AppMusicIdentityDbContext(DbContextOptions<AppMusicIdentityDbContext> options)
		: base(options) { }
	}
}