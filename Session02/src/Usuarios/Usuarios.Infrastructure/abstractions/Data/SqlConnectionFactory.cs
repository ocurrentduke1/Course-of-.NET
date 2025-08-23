using System.Data;
using Npgsql;
using Usuarios.Application.Abstractions.Data;

namespace Usuarios.Infrastructure.abstractions.Data;

internal class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connection;

    public SqlConnectionFactory(string connection)
    {
        _connection = connection;
    }

    public IDbConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(_connection);
        connection.Open();
        return connection;
    }
}