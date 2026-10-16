using GameLibrary.Data.Models;
namespace GameLibrary.Data.Repositories;
public interface IPlatformRepository
{
    Task<List<Platform>> GetAllAsync(string? search = null);
    Task<Platform?> GetByIdAsync(int platformId);
    Task<int> AddAsync(Platform platform);
    Task<bool> UpdateAsync(Platform platform);
    Task<bool> DeleteAsync(int platformId);
}
