using MongoDB.Driver;
using WebAPI.Models;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMongoDatabase>(options =>
{
    var settings = MongoClientSettings.FromConnectionString("mongodb+srv://yasintasci:Yasin2904@cluster0.nv3qvwf.mongodb.net/?retryWrites=true&w=majority");
    var client = new MongoClient(settings);
    var database = client.GetDatabase("Hurriyet");
    return database;
});
builder.Services.AddSingleton<ICategoryService,CategoryService>();
builder.Services.AddSingleton<IPostService, PostService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
