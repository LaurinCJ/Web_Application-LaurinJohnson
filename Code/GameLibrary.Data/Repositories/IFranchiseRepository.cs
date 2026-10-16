using GameLibrary.Data.Models;
namespace GameLibrary.Data.Repositories;
public interface IFranchiseRepository
{
    Task<List<Franchise>> GetAllAsync(string? search = null);
    Task<Franchise?> GetByIdAsync(int franchiseId);
    Task<int> AddAsync(Franchise franchise);
    Task<bool> UpdateAsync(Franchise franchise);
    Task<bool> DeleteAsync(int franchiseId);
}
