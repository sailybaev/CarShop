using Npgsql;

namespace CarShop.Database;

public class BbConnection
{
    private readonly string _connStr;

    public BbConnection(string connStr)
    {
        _connStr = connStr;
    }

    public NpgsqlConnection GetConnection()
    {
        var conn = new NpgsqlConnection(_connStr);
        conn.Open();
        return conn;
    }
}