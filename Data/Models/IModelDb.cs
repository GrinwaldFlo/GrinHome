namespace GrinHome.Data.Models
{
	public interface IModelDb<T>
	{
		Task<bool> AddAsync(ApplicationDbContext db);
		Task<bool> UpdateAsync(ApplicationDbContext db);
		Task<bool> DeleteAsync(ApplicationDbContext db);
	}
}
