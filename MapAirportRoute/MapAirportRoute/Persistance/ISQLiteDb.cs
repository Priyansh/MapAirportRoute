using SQLite;

namespace MapAirportRoute
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}

