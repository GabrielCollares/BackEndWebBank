using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BackendSharp.Users;


public class AppDbContext(IConfiguration config) : DbContext
{
    public IConfiguration Configuration { get; } = config;

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
        optionsBuilder.UseSqlite(Configuration.GetConnectionString("Default"));
        base.OnConfiguring(optionsBuilder);
    }
}

