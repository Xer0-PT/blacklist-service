using BlackList.Api.Extensions;
using BlackList.Application.Abstractions;
using BlackList.Application.Services;
using BlackList.Persistence.Data;
using BlackList.Persistence.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("BlackList");

// Add AutoMapper
builder.Services.AddSingleton(AddAutoMapperConfig.Initialize());

// Add DbContext
builder.Services.AddDbContext<BlackListServiceDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// Add Persistence Services
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IBlackListRepository, BlackListRepository>();

// Add services to the container
builder.Services.AddSingleton<Random>();
builder.Services.AddScoped<IBlackListService, BlackListService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
