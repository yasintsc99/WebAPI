using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class JWTAuthService : IJWTAuthService
    {
        private readonly IMongoCollection<UserLogin> _userCollection;

        public JWTAuthService(IMongoDatabase database)
        {
            _userCollection = database.GetCollection<UserLogin>("UserLogin");
        }
        public string Authenticate(string username, string password)
        {
            if (!_userCollection.Find(x => x.UserName == username && x.Password == password).Any())
            {
                return "User Not Found";
            }
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.ASCII.GetBytes("ASPNetCoreWebApiAuthorizationWithJsonWebToken");
            var tokendescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(type: ClaimTypes.Name, value: username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenkey), algorithm: SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokendescriptor);
            return tokenhandler.WriteToken(token);
        }
    }
}
