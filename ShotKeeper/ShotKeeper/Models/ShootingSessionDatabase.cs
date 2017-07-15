using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;


namespace ShotKeeper.Models
{
    public class ShootingSessionDatabase
    {
        readonly SQLiteAsyncConnection database;

        public ShootingSessionDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ShootingSession>().Wait();
        }

        public Task<List<ShootingSession>> GetItemsAsync()
        {
            return database.Table<ShootingSession>().ToListAsync();
        }
        
        public Task<List<ShootingSession>> GetAllItemsForListDisplayAsync()
        {
            return database.QueryAsync<ShootingSession>("SELECT * FROM [ShootingSession]");
        }

        public Task<ShootingSession> GetItemAsync(int id)
        {
            return database.Table<ShootingSession>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(ShootingSession item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(ShootingSession item)
        {
            return database.DeleteAsync(item);
        }
    }
}
