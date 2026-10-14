using Microsoft.Data.SqlClient;

namespace GameLibrary.Data.Repositories;

public class DatabaseConnection
{
    private readonly string _connectionString;
    public DatabaseConnection(string connectionString) => _connectionString = connectionString;
    public SqlConnection CreateConnection() => new(_connectionString);
}
