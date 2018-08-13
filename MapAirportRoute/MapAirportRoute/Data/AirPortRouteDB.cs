using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace MapAirportRoute
{
    public class AirPortRouteDB
    {
        readonly SQLiteAsyncConnection database;
        public AirPortRouteDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<AirPorts>().Wait();
        }

        public Task<List<AirPorts>> GetItemsAsync()
        {
            return database.Table<AirPorts>().ToListAsync();
        }

        public Task<List<AirPorts>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<AirPorts>("SELECT * FROM [AirPorts] WHERE [Done] = 0");
        }

        //public Task<AirPorts> GetItemAsync(int id)
        //{
        //    return database.Table<AirPorts>().Where(i => i.ID == id).FirstOrDefaultAsync();
        //}

        //public Task<int> SaveItemAsync(AirPorts item)
        //{
        //    if (item.ID != 0)
        //    {
        //        return database.UpdateAsync(item);
        //    }
        //    else
        //    {
        //        return database.InsertAsync(item);
        //    }
        //}

        public Task<int> DeleteItemAsync(AirPorts item)
        {
            return database.DeleteAsync(item);
        }
    }
}
