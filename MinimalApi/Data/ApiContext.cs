using Microsoft.EntityFrameworkCore;
using MinimalApi.Models;
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

    /* dotnet ef migrations add InitialCreate --project .\Persistence\ --startup-project .\API\ --output-dir ./Data/Migrations 
       */
    /* dotnet ef database update --project .\Persistence\ --startup-project .\API\
     */
}
