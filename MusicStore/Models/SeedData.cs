using Microsoft.EntityFrameworkCore;
using MusicStore.Models;

namespace MusicStore.Models
{
	public static class SeedData
	{
		public static void EnsurePopulated(IApplicationBuilder app)
		{
			StoreDbContext context = app.ApplicationServices
			.CreateScope().ServiceProvider.GetRequiredService<StoreDbContext>();
			if (context.Database.GetPendingMigrations().Any())
			{
				context.Database.Migrate();
			}
			if (!context.Products.Any())
			{
				context.Products.AddRange(
				new Product
				{
					Name = "Fender Stratocaster",
					Description = "Iconic electric guitar known for its versatility",
					Category = "Guitars",
					Price = 1499.99m
				},
new Product
{
	Name = "Taylor 814ce",
	Description = "High-end acoustic guitar with exceptional sound and craftsmanship",
	Category = "Guitars",
	Price = 3499.99m
},
new Product
{
	Name = "Roland TD-17KVX",
	Description = "Professional electronic drum set with advanced features",
	Category = "Percussion",
	Price = 1799.99m
},
new Product
{
	Name = "LP Aspire Conga Set",
	Description = "Beginner-friendly conga drums for Latin percussion",
	Category = "Percussion",
	Price = 399.99m
},
new Product
{
	Name = "Yamaha YDP-144",
	Description = "Digital piano with realistic sound and weighted keys",
	Category = "Pianos",
	Price = 1099.99m
},
new Product
{
	Name = "Steinway Model D",
	Description = "Concert grand piano renowned for its rich sound and craftsmanship",
	Category = "Pianos",
	Price = 99999.99m
},
new Product
{
	Name = "The Guitar Handbook",
	Description = "Comprehensive guide to learning and mastering the guitar",
	Category = "Books",
	Price = 29.99m
},
new Product
{
	Name = "The Drummer's Bible",
	Description = "Essential resource for drummers covering techniques, rhythms, and more",
	Category = "Books",
	Price = 24.99m
},
new Product
{
	Name = "Piano Adventures",
	Description = "Popular method book series for piano learners of all ages",
	Category = "Books",
	Price = 12.99m
},
new Product
{
	Name = "The Complete Idiot's Guide to Music Theory",
	Description = "Beginner-friendly guide to understanding music theory concepts",
	Category = "Books",
	Price = 19.99m
}

				);
				context.SaveChanges();
			}
		}
	}
}