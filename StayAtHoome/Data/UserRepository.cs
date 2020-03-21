using System.Threading;
using System.Threading.Tasks;
using SQLite;
using StayAtHoome.Models;

namespace StayAtHoome.Data
{
    public class UserRepository
    {
        public AsyncTableQuery<User> Query => LocalDatabase.Database.Table<User>();

        public Task<User> GetUserAsync()
        {
            return Query.FirstOrDefaultAsync();
        }

        public async Task<User> CreateUserAsync(string name)
        {
            var user = new User() {Name = name};
            var database = LocalDatabase.Database;
            await database.InsertAsync(user);

            return user;
        }
    }
}