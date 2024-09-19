using Microsoft.EntityFrameworkCore;
using MinimalApi.Models.Entities;
using System.Reflection;

namespace MinimalApi.Data;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options)
    {

    }

    public DbSet<Rol> Rols { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRol> userRols { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /* dotnet ef migrations add InitialCreate --project .\MinimalApi --startup-project .\MinimalApi\ --output-dir ./Data/Migrations
       */
    /* dotnet ef database update --project .\MinimalApi\ --startup-project .\MinimalApi\
     */
}
