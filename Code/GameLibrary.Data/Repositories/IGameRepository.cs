using GameLibrary.Data.Models;
namespace GameLibrary.Data.Repositories;
public interface IGameRepository
{
    Task<List<Game>> GetAllAsync(string? titleSearch = null, string? franchiseSearch = null);
    Task<Game?> GetByIdAsync(int gameId);
    Task<int> AddAsync(Game game);
    Task<bool> UpdateAsync(Game game);
    Task<bool> DeleteAsync(int gameId);
}
