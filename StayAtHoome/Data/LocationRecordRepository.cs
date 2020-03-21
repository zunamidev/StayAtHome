using System;
using System.Threading.Tasks;
using SQLite;
using StayAtHoome.Models;

namespace StayAtHoome.Data
{
    public class LocationRecordRepository
    {
        private readonly SQLiteAsyncConnection _database = LocalDatabase.Database;


        public async Task CreateLocationRecord(LocationRecord record)
        {
            await LocalDatabase.WaitInitialized;
            await this._database.InsertAsync(record);

            var records = await this._database.Table<LocationRecord>().CountAsync();
            
            Console.WriteLine($"We have {records} location records");
        }
    }
}