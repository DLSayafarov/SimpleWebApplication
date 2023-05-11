using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess;

public class UsersContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public UsersContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // только hardcode, только хардкор
        // TODO доставать конфигурационную строку из appsettings
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=users_database;Username=postgres;Password=postgres");
        base.OnConfiguring(optionsBuilder);
    }
}