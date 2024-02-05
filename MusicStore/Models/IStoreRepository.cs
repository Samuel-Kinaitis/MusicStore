using MusicStore.Models;

namespace MusicStore.Models
{
	public interface IStoreRepository
	{
		IQueryable<Product> Products { get; }
	}
}