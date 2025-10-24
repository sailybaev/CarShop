using Npgsql;

namespace CarShop.Database;
// Eto dlya bazy dannykh
public class BbConnection
{
    private readonly string _url;
    
    public BbConnection(string url)
    {
        _url = url;
    }

    public NpgsqlConnection GetConnection()
    {
        var conn = new NpgsqlConnection(_url);
        conn.Open();
        return conn;
    }
}