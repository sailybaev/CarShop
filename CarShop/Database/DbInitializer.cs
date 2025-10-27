using Npgsql;

namespace CarShop.Database;

public static class DbInitializer
{
    public static void Initialize(BbConnection bbConnection)
    {
        using var conn = bbConnection.GetConnection();

        // Create Users table
        using (var cmd = new NpgsqlCommand(@"
            CREATE TABLE IF NOT EXISTS users (
                username TEXT PRIMARY KEY,
                password TEXT NOT NULL,
                role TEXT NOT NULL,
                balance NUMERIC NOT NULL
            )", conn))
        {
            cmd.ExecuteNonQuery();
        }

        // Create Cars table
        using (var cmd = new NpgsqlCommand(@"
            CREATE TABLE IF NOT EXISTS cars (
                id SERIAL PRIMARY KEY,
                name TEXT NOT NULL,
                price NUMERIC NOT NULL,
                owner_username TEXT NULL REFERENCES users(username)
            )", conn))
        {
            cmd.ExecuteNonQuery();
        }
    }
}

