using AspNetCoreRateLimit;
using BlackList.Api.Extensions;
using BlackList.Application.Abstractions;
using BlackList.Application.Services;
using BlackList.Persistence.Data;
using BlackList.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Refit;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("BlackList");
var faceitUri = builder.Configuration.GetValue<string>("FaceitApiConfig:Url");

// Add AutoMapper
builder.Services.AddSingleton(AddAutoMapperConfig.Initialize());

// Add DbContext
builder.Services.AddDbContext<BlackListServiceDbContext>(opt =>
{
    opt.UseNpgsql(
        connectionString,
        options => options.EnableRetryOnFailure());
});

// Add rate limiting configuration
var ipRateLimiting = builder.Configuration.GetSection("IpRateLimiting");
var ipRateLimitPolicies = builder.Configuration.GetSection("IpRateLimitPolicies");
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(ipRateLimiting);
builder.Services.Configure<IpRateLimitPolicies>(ipRateLimitPolicies);
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

// Add Persistence Services
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IBlackListedPlayerRepository, BlackListedPlayerRepository>();

// Add services to the container
builder.Services.AddScoped<IBlackListedPlayerService, BlackListedPlayerService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddScoped<IFaceitGateway, FaceitGateway>();
builder.Services.AddTransient<AuthHeaderHandler>();
builder.Services.AddRefitClient<IFaceitApi>()
    .AddHttpMessageHandler<AuthHeaderHandler>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(faceitUri);
    });

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS policy
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.WithOrigins(builder.Configuration["AllowedHosts"]);
    });
});

// Add Health Checks
builder.Services
    .AddHealthChecks()
    .AddDbContextCheck<BlackListServiceDbContext>();

var app = builder.Build();

app.UseIpRateLimiting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/api/healthz");

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
