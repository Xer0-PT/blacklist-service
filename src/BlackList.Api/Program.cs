using AspNetCoreRateLimit;
using BlackList.Api.Extensions;
using BlackList.Persistence.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(AddAutoMapperConfig.Initialize());
// builder.Services.AddRateLimitingServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services
    .AddHealthChecks()
    .AddDbContextCheck<BlackListServiceDbContext>();

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

var app = builder.Build();

// app.UseIpRateLimiting();

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

public partial class Program { }