using System;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using StayAtHoome.Models;

namespace StayAtHoome.Data
{
    public class LocalDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> LazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags)
            {
                
            };
        });

        public static SQLiteAsyncConnection Database => LazyInitializer.Value;
        static bool _initialized;

        public static Task WaitInitialized { get; }

        static LocalDatabase()
        {
            WaitInitialized = InitializeAsync();
        }
        
        private static async Task InitializeAsync()
        {
            if (!_initialized)
            {
                await Database.CreateTablesAsync(CreateFlags.None, typeof(User)).ConfigureAwait(false);
                await Database.CreateTablesAsync(CreateFlags.None, typeof(LocationRecord)).ConfigureAwait(false);
                _initialized = true;
            }
        }

        public static async void Clear()
        {
            await WaitInitialized;

            await Database.DeleteAllAsync<User>();
            await Database.DeleteAllAsync<LocationRecord>();
        }
    }
}