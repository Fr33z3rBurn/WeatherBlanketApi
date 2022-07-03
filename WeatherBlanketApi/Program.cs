using Microsoft.Extensions.Options;
using WeatherBlanketApi.Models;
using WeatherBlanketApi.Services;

var builder = WebApplication.CreateBuilder(args);
var AllowSpecificOrigins = "_myAllowSpecificOrigins";
var AllowAllOrigins = "_myAllowAllOrigins";

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<UserDatabaseSettings>(
				builder.Configuration.GetSection(nameof(UserDatabaseSettings)));

builder.Services.AddSingleton<IUserDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<UserDatabaseSettings>>().Value);

builder.Services.AddSingleton<IUserService, UserService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowAllOrigins,
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(AllowAllOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
