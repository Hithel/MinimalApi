using AspNetCoreRateLimit;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Data;
using MinimalApi.Extensions.CorsConfiguration;
using MinimalApi.Extensions.JwtTokenConfiguration;
using MinimalApi.Extensions.RateLimitingConfiguration;
using MinimalApi.Extensions.RepositoryRegistration;
using MinimalApi.Extensions.ServiceRegistration;
using Serilog;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(builder.Configuration)
                    .Enrich.FromLogContext()
                    .CreateLogger();


// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.AddMvc();
builder.Services.ConfigureRateLimit();
builder.Services.AddJwt(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("ConexMysql")!;
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseIpRateLimiting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
