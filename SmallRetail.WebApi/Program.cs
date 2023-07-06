using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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
builder.Configuration.AddEnvironmentVariables(prefix: "APP_");
var config = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

var audience = config.GetValue<string>("OAUTH_CLIENT_ID");
var issuer = config.GetValue<string>("OAUTH_ISSUER");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Audience = audience;
        options.Authority = issuer;
    });
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmallRetail API" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. <br>
                      Enter 'Bearer' [space] and then your token in the text input below. <br>
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });
});

// Connect to database
var host = config.GetValue<string>("DB_HOST");
var user = config.GetValue<string>("DB_USER");
var password = config.GetValue<string>("DB_PASS");
var database = config.GetValue<string>("DB_NAME");
var port = config.GetValue<int>("DB_PORT");
var connectionString = $"Host={host}; Port={port}; Database={database}; Username={user}; Password={password}";
builder.Services.AddDbContextPool<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

var app = builder.Build();

await using var scope = app.Services.CreateAsyncScope();
using var db = scope.ServiceProvider.GetService<AppDbContext>();
if (db != null)
{
    await db.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
