using System.Threading.Tasks;
using SQLite;
using StayAtHoome.Models;

namespace StayAtHoome.Data
{
    public delegate void UserChangedEvent();

    public class UserRepository
    {
        public event UserChangedEvent UserChanged;

        private readonly SQLiteAsyncConnection _database = LocalDatabase.Database;

        public async Task<User> GetUserAsync()
        {
            await LocalDatabase.WaitInitialized;
            return await _database.Table<User>().FirstOrDefaultAsync();
        }

        public async Task CreateUserAsync(string name)
        {
            await LocalDatabase.WaitInitialized;
            var user = new User { Name = name };
            await _database.InsertAsync(user);

            UserChanged?.Invoke();
        }
    }
}