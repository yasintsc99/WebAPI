namespace WebAPI.Services
{
    public interface IJWTAuthService
    {
        string Authenticate(string username, string password);
    }
}
