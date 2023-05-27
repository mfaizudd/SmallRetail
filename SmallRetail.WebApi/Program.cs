using Microsoft.EntityFrameworkCore;
using SmallRetail.WebApi.Data;
using SmallRetail.WebApi.Helpers;
using SmallRetail.WebApi.Services;

// Load .env
DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShopService, ShopService>();

// Connect to database
builder.Configuration.AddEnvironmentVariables(prefix: "APP_");
var config = builder.Configuration;
var host = config.GetValue<string>("DB_HOST");
var user = config.GetValue<string>("DB_USER");
var password = config.GetValue<string>("DB_PASS");
var database = config.GetValue<string>("DB_NAME");
var port = config.GetValue<int>("DB_PORT");
var connectionString = $"Host={host}; Port={port}; Database={database}; Username={user}; Password={password}";
builder.Services.AddDbContextPool<SmallRetail.WebApi.Data.AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
