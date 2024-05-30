using Microsoft.Data.Sqlite;
using RamenStore.Application.Abstractions.Data;
using System.Data;

namespace RamenStore.Infrastructure.Data;


internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        var connection = new SqliteConnection(_connectionString);
        connection.Open();

        return connection;
    }
}