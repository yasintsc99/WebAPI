using MongoDB.Driver;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IMongoCollection<UserLogin> _userCollection;
        public UserLoginService(IMongoDatabase mongoDatabase)
        {
            _userCollection = mongoDatabase.GetCollection<UserLogin>("UserLogin");
        }

        public async Task<UserLogin> FindUser(string username, string password)
        {
            return await _userCollection.Find(x => x.UserName == username && x.Password == password).FirstOrDefaultAsync();
        }
    }
}
