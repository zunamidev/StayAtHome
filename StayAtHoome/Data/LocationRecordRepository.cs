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
            await _database.InsertAsync(record);

            var records = await _database.Table<LocationRecord>().CountAsync();
            
            Console.WriteLine($"We have {records} location records");
        }

        public async Task<LocationRecord[]> GetLocationRecordsAsync(DateTimeOffset from)
        {
            return await _database.Table<LocationRecord>()
                .Where(x => x.Timestamp >= from)
                .OrderBy(x => x.Timestamp)
                .ToArrayAsync();
        }

        public async Task Clear()
        {
            await _database.DeleteAllAsync(await _database.GetMappingAsync<LocationRecord>());
        }

        public Task<int> Count()
        {
            return _database.Table<LocationRecord>().CountAsync();
        }
    }
}