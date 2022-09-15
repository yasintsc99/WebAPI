using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IUserLoginService
    {
        Task<UserLogin> FindUser(string username, string password);
    }
}
