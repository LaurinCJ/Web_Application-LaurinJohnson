using GameLibrary.Data.Models;
using Microsoft.Data.SqlClient;

namespace GameLibrary.Data.Repositories;

public class PlatformRepository : IPlatformRepository
{
    private readonly DatabaseConnection _database;
    public PlatformRepository(DatabaseConnection database) => _database = database;
    public async Task<List<Platform>> GetAllAsync() => await GetManyAsync("SELECT PlatformId, PlatformName, Manufacturer FROM Platforms ORDER BY PlatformName;");
    public async Task<Platform?> GetByIdAsync(int platformId) => (await GetManyAsync("SELECT PlatformId, PlatformName, Manufacturer FROM Platforms WHERE PlatformId=@Id;", platformId)).SingleOrDefault();
    public async Task<int> AddAsync(Platform platform) => await InsertAsync("INSERT INTO Platforms (PlatformName, Manufacturer) VALUES (@Name, @Manufacturer); SELECT CAST(SCOPE_IDENTITY() AS int);", platform);
    public async Task<bool> UpdateAsync(Platform platform) => await ChangeAsync("UPDATE Platforms SET PlatformName=@Name, Manufacturer=@Manufacturer WHERE PlatformId=@Id;", platform, true);
    public async Task<bool> DeleteAsync(int platformId) => await DeleteByIdAsync("DELETE FROM Platforms WHERE PlatformId=@Id;", platformId);

    private async Task<List<Platform>> GetManyAsync(string sql, int? id = null) { var list=new List<Platform>(); await using var c=_database.CreateConnection(); await using var cmd=new SqlCommand(sql,c); if(id.HasValue)cmd.Parameters.AddWithValue("@Id",id.Value); await c.OpenAsync(); await using var r=await cmd.ExecuteReaderAsync(); while(await r.ReadAsync())list.Add(new Platform{PlatformId=r.GetInt32(0),PlatformName=r.GetString(1),Manufacturer=r.GetString(2)}); return list; }
    private async Task<int> InsertAsync(string sql, Platform p) { await using var c=_database.CreateConnection(); await using var cmd=new SqlCommand(sql,c); AddParameters(cmd,p); await c.OpenAsync(); return (int)(await cmd.ExecuteScalarAsync())!; }
    private async Task<bool> ChangeAsync(string sql, Platform p, bool includeId) { await using var c=_database.CreateConnection(); await using var cmd=new SqlCommand(sql,c); AddParameters(cmd,p); if(includeId)cmd.Parameters.AddWithValue("@Id",p.PlatformId); await c.OpenAsync(); return await cmd.ExecuteNonQueryAsync()>0; }
    private async Task<bool> DeleteByIdAsync(string sql,int id) { await using var c=_database.CreateConnection(); await using var cmd=new SqlCommand(sql,c); cmd.Parameters.AddWithValue("@Id",id); await c.OpenAsync(); return await cmd.ExecuteNonQueryAsync()>0; }
    private static void AddParameters(SqlCommand c, Platform p) { c.Parameters.AddWithValue("@Name",p.PlatformName); c.Parameters.AddWithValue("@Manufacturer",p.Manufacturer); }
}
