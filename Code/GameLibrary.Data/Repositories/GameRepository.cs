using GameLibrary.Data.Models;
using Microsoft.Data.SqlClient;

namespace GameLibrary.Data.Repositories;

public class GameRepository : IGameRepository
{
    private readonly DatabaseConnection _database;
    public GameRepository(DatabaseConnection database) => _database = database;

    public async Task<List<Game>> GetAllAsync(string? titleSearch = null, string? franchiseSearch = null)
    {
        const string sql = """
            
            SELECT g.GameId, g.GameTitle, g.ReleaseYear, g.FranchiseId, g.PlatformId,
                   f.FranchiseName, p.PlatformName
            FROM Games g
            JOIN Franchises f ON g.FranchiseId = f.FranchiseId
            JOIN Platforms p ON g.PlatformId = p.PlatformId
            WHERE (@Title IS NULL OR g.GameTitle LIKE '%' + @Title + '%')
              AND (@Franchise IS NULL OR f.FranchiseName LIKE '%' + @Franchise + '%')
            ORDER BY g.GameTitle;
            
            """;

        var games = new List<Game>();
        await using var connection = _database.CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Title", string.IsNullOrWhiteSpace(titleSearch) ? DBNull.Value : titleSearch);
        command.Parameters.AddWithValue("@Franchise", string.IsNullOrWhiteSpace(franchiseSearch) ? DBNull.Value : franchiseSearch);
        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync()) games.Add(ReadGame(reader));
        return games;
    }

    public async Task<Game?> GetByIdAsync(int gameId)
    {
        const string sql = "SELECT g.GameId, g.GameTitle, g.ReleaseYear, g.FranchiseId, g.PlatformId, f.FranchiseName, p.PlatformName FROM Games g JOIN Franchises f ON g.FranchiseId = f.FranchiseId JOIN Platforms p ON g.PlatformId = p.PlatformId WHERE g.GameId = @Id;";

        await using var connection = _database.CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", gameId);

        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();

        return await reader.ReadAsync() ? ReadGame(reader) : null;
    }

    public async Task<int> AddAsync(Game game)
    {
        const string sql = "INSERT INTO Games (GameTitle, ReleaseYear, FranchiseId, PlatformId) VALUES (@Title, @Year, @FranchiseId, @PlatformId); SELECT CAST(SCOPE_IDENTITY() AS int);";
        await using var connection = _database.CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        AddParameters(command, game);
        await connection.OpenAsync();
        return (int)(await command.ExecuteScalarAsync())!;
    }

    public async Task<bool> UpdateAsync(Game game)
    {
        const string sql = "UPDATE Games SET GameTitle=@Title, ReleaseYear=@Year, FranchiseId=@FranchiseId, PlatformId=@PlatformId WHERE GameId=@Id;";
        await using var connection = _database.CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        AddParameters(command, game); command.Parameters.AddWithValue("@Id", game.GameId);
        await connection.OpenAsync();
        return await command.ExecuteNonQueryAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int gameId)
    {
        await using var connection = _database.CreateConnection();
        await using var command = new SqlCommand("DELETE FROM Games WHERE GameId=@Id;", connection);
        command.Parameters.AddWithValue("@Id", gameId);
        await connection.OpenAsync();
        return await command.ExecuteNonQueryAsync() > 0;
    }

    private static void AddParameters(SqlCommand command, Game game)
    {
        command.Parameters.AddWithValue("@Title", game.GameTitle);
        command.Parameters.AddWithValue("@Year", game.ReleaseYear);
        command.Parameters.AddWithValue("@FranchiseId", game.FranchiseId);
        command.Parameters.AddWithValue("@PlatformId", game.PlatformId);
    }
    private static Game ReadGame(SqlDataReader reader) => new()
    {
        GameId = reader.GetInt32(0), GameTitle = reader.GetString(1), ReleaseYear = reader.GetInt32(2),
        FranchiseId = reader.GetInt32(3), PlatformId = reader.GetInt32(4), FranchiseName = reader.GetString(5), PlatformName = reader.GetString(6)
    };
}
