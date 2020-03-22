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

        public async Task UpdateUserAsync(User user)
        {
            await LocalDatabase.WaitInitialized;
            if (user.Id == 0)
            {
                await _database.InsertAsync(user);
            }
            else
            {
                await _database.UpdateAsync(user);
            }

            UserChanged?.Invoke();
        }
    }
}