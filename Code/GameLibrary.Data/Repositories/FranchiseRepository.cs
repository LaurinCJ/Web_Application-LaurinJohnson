using GameLibrary.Data.Models;
using Microsoft.Data.SqlClient;

namespace GameLibrary.Data.Repositories;

public class FranchiseRepository : IFranchiseRepository
{
    private readonly DatabaseConnection _database;
    public FranchiseRepository(DatabaseConnection database) => _database = database;
    public async Task<List<Franchise>> GetAllAsync() => await GetManyAsync("SELECT FranchiseId, FranchiseName, Publisher FROM Franchises ORDER BY FranchiseName;");
    public async Task<Franchise?> GetByIdAsync(int franchiseId) => (await GetManyAsync("SELECT FranchiseId, FranchiseName, Publisher FROM Franchises WHERE FranchiseId=@Id;", franchiseId)).SingleOrDefault();
    public async Task<int> AddAsync(Franchise franchise) => await InsertAsync("INSERT INTO Franchises (FranchiseName, Publisher) VALUES (@Name, @Publisher); SELECT CAST(SCOPE_IDENTITY() AS int);", franchise);
    public async Task<bool> UpdateAsync(Franchise franchise) => await ChangeAsync("UPDATE Franchises SET FranchiseName=@Name, Publisher=@Publisher WHERE FranchiseId=@Id;", franchise, true);
    public async Task<bool> DeleteAsync(int franchiseId) => await DeleteByIdAsync("DELETE FROM Franchises WHERE FranchiseId=@Id;", franchiseId);

    private async Task<List<Franchise>> GetManyAsync(string sql, int? id = null) { var list=new List<Franchise>(); await using var c=_database.CreateConnection(); await using var cmd=new SqlCommand(sql,c); if(id.HasValue)cmd.Parameters.AddWithValue("@Id",id.Value); await c.OpenAsync(); await using var r=await cmd.ExecuteReaderAsync(); while(await r.ReadAsync())list.Add(new Franchise{FranchiseId=r.GetInt32(0),FranchiseName=r.GetString(1),Publisher=r.GetString(2)}); return list; }
    private async Task<int> InsertAsync(string sql, Franchise f) { await using var c=_database.CreateConnection(); await using var cmd=new SqlCommand(sql,c); AddParameters(cmd,f); await c.OpenAsync(); return (int)(await cmd.ExecuteScalarAsync())!; }
    private async Task<bool> ChangeAsync(string sql, Franchise f, bool includeId) { await using var c=_database.CreateConnection(); await using var cmd=new SqlCommand(sql,c); AddParameters(cmd,f); if(includeId)cmd.Parameters.AddWithValue("@Id",f.FranchiseId); await c.OpenAsync(); return await cmd.ExecuteNonQueryAsync()>0; }
    private async Task<bool> DeleteByIdAsync(string sql,int id) { await using var c=_database.CreateConnection(); await using var cmd=new SqlCommand(sql,c); cmd.Parameters.AddWithValue("@Id",id); await c.OpenAsync(); return await cmd.ExecuteNonQueryAsync()>0; }
    private static void AddParameters(SqlCommand c, Franchise f) { c.Parameters.AddWithValue("@Name",f.FranchiseName); c.Parameters.AddWithValue("@Publisher",f.Publisher); }
}
