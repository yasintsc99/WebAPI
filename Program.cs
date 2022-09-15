using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ASPNetCoreWebApiAuthorizationWithJsonWebToken"))
    };
});
builder.Services.AddSingleton<IMongoDatabase>(options =>
{
    var settings = MongoClientSettings.FromConnectionString("mongodb+srv://yasintasci:Yasin2904@cluster0.nv3qvwf.mongodb.net/?retryWrites=true&w=majority");
    var client = new MongoClient(settings);
    var database = client.GetDatabase("Hurriyet");
    return database;
});
builder.Services.AddSingleton<ICategoryService, CategoryService>();
builder.Services.AddSingleton<IPostService, PostService>();
builder.Services.AddSingleton<IJWTAuthService, JWTAuthService>();
builder.Services.AddSingleton<IUserLoginService, UserLoginService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
